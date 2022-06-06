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
    public bool moving;//wether or not it moves around physically
    Vector3 aiDestination; //the point where the AI will navigate to if 1. moving is true 2. it's not 0,0,0

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
        //calculate destination
        if (wandering)
        {
            if (Vector3.Distance(transform.position, aiDestination) < 0.1f) //we make the character stop wandering if it's too close to the destination
            {
                moving = false;
            }
            timeSinceLastWander += Time.deltaTime; //check if it's time to wander
            if (timeSinceLastWander >= moveDelay)
            {//move to a random position
                moving = true;
                aiDestination = new Vector3(transform.position.x + Random.Range(0, moveDistance), transform.position.y, transform.position.z + Random.Range(0, moveDistance));
            }

            if (Vector3.Distance(transform.position, startingPosition) > (3 * moveDistance)) //we make the character stop wandering if it's too close to the destination
            {

                moving = true;
                aiDestination = startingPosition;
            }
        }


        //just move
        if (!player && moving)
        {
            navAgent.SetDestination(aiDestination);
        }
        
        
    }



    

    public void Movement(Vector3 movePoint) // moves towards a point in a set speed 
    {
        movePoint.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, movePoint, moveSpeed * Time.deltaTime);
    }




    public void MoveTo(Vector3 Destination)
    {
        moving = true;
        navAgent.SetDestination(Destination);

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
