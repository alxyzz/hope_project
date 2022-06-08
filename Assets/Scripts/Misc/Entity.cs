using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{

    public string entityName, description;

    public bool player = false;
    public bool talks; //in case it does not want to talk
    public bool animating;//used for animation
    public bool wandering;//wether it periodically moves around in an area
    Vector3 startingPosition; //used to track starting position so we don't go too far from it while wandering

    public float moveDelay;//for random periodic movement if necessary
    public float moveDistance;//the amount it moves if it moves randomly 

    public float moveSpeed;
    public Rigidbody rigidBody;

    //navigation
    public NavMeshAgent navAgent;
    Vector3 aiDestination; //the point where the AI will navigate to if 1. moving is true 2. it's not 0,0,0
    //for pausing and unpausing
    Vector3 lastAgentVelocity;
    NavMeshPath lastAgentPath;
    //talk to
    public UnityEvent talkFunction;//this can be changed to whatever you want to happen when you interact with this guy


    /// <summary>
    /// we will call Message("Interact") on whatever NPC or object we want to interact with later so this has the same name as the object function
    /// </summary>
    public void Interact()
    {
        talkFunction.Invoke();
    }

    public void Start()
    {
        startingPosition = transform.position;
    }

    private float timeSinceLastWander;
    // Update is called once per frame






    void Update()
    {
        if (wandering)
        {
            if (Vector3.Distance(transform.position, aiDestination) < 0.1f) //we make the character stop wandering if it's too close to the destination
            {
                PauseMovement();
            }
            timeSinceLastWander += Time.deltaTime; //check if it's time to wander
            if (timeSinceLastWander >= moveDelay)
            {//move to a random position
                ResumeMovement();
                aiDestination = new Vector3(transform.position.x + Random.Range(0, moveDistance), transform.position.y, transform.position.z + Random.Range(0, moveDistance));

            }

            if (Vector3.Distance(transform.position, startingPosition) > (3 * moveDistance)) //we make the character stop wandering if it's too close to the destination
            {

                ResumeMovement();
                aiDestination = startingPosition;
            }
        }


        //if (!player && moving)
        //{
        //    navAgent.SetDestination(aiDestination);
        //}


    }




    public void PauseMovement()
    {

        Debug.Log("ran pausemovement");
        lastAgentPath = navAgent.path;
        lastAgentVelocity = navAgent.velocity;
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;




    }

    public void ResumeMovement()
    {
        Debug.Log("ran resumemovement");
        navAgent.isStopped = false;
        navAgent.velocity = lastAgentVelocity;
        navAgent.SetPath(lastAgentPath);
    }








    public void Movement(Vector3 movePoint) // moves towards a hit point (Player)
    {
        navAgent.SetDestination(movePoint);
    }



    private void OnTriggerEnter(Collider other)
    {//if you enter the range of an item, and it is an item, you select it for interaction. CAREFUL - this means we can't have objects too close together
        if (player)
        {
            if (other.gameObject.GetComponent<GenericObject>() != null)
            {
                other.gameObject.GetComponent<GenericObject>().Select(true);

            }
        }

    }

    private void OnTriggerExit(Collider other)
    {//if you leave the range of an item, and it is an item, you deselect it
        if (player)
        {
            if (other.gameObject.GetComponent<GenericObject>() != null)
            {
                other.gameObject.GetComponent<GenericObject>().Select(false);
            }
        }

    }


}
