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
    public bool lastMovementWasRight;
    public float moveSpeed;
    public Rigidbody rigidBody;


    Vector3 lastpos;
    private SpriteRenderer sprenderer;

    Animator animRef;
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
        if (!player)
        {
            talkFunction.Invoke();
        }
        
    }


    public void Start()
    {
        sprenderer = GetComponent<SpriteRenderer>();
        try
        {
            if (player)
            {
                animRef = GetComponent<Animator>();
            }
        }
        catch
        {
            Debug.Log("entity " + entityName + " has no animator.");
        }
        
        startingPosition = transform.position;
    }

    //private float timeSinceLastWander;

    void Update()
    {
        //if (wandering)
        //{
        //    if (Vector3.Distance(transform.position, aiDestination) < 0.1f) //we make the character stop wandering if it's too close to the destination
        //    {
        //        PauseMovement();
        //    }
        //    timeSinceLastWander += Time.deltaTime; //check if it's time to wander
        //    if (timeSinceLastWander >= moveDelay)
        //    {//move to a random position
        //        ResumeMovement();
        //        aiDestination = new Vector3(transform.position.x + Random.Range(0, moveDistance), transform.position.y, transform.position.z + Random.Range(0, moveDistance));

        //    }

        //    if (Vector3.Distance(transform.position, startingPosition) > (3 * moveDistance)) //we make the character stop wandering if it's too close to the destination
        //    {

        //        ResumeMovement();
        //        aiDestination = startingPosition;
        //    }
        //}
        if (player)
        {
            if (transform.position.x > lastpos.x)
            {//moved left
                animRef.SetBool("walkLeft", true);
                animRef.SetBool("walkRight", false);
                animRef.SetBool("lookLeft", false);
                lastMovementWasRight = false;

            }
            else if (transform.position.x < lastpos.x)
            {//moved right
                animRef.SetBool("walkRight", true);
                animRef.SetBool("walkLeft", false);
                animRef.SetBool("lookLeft", false);
                lastMovementWasRight = true;
            }

            if (lastpos == transform.position)
            {
                animRef.SetBool("walkRight", false);
                animRef.SetBool("walkLeft", false);
                if (lastMovementWasRight)
                {
                    animRef.SetBool("lookLeft", false);
                }
                else
                {
                    animRef.SetBool("lookLeft", true);
                }

            }
            lastpos = transform.position;
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

        animRef.SetBool("walkRight", false);
        animRef.SetBool("walkLeft", false);




    }

    public void ResumeMovement()
    {
        Debug.Log("ran resumemovement");
        navAgent.isStopped = false;
        navAgent.velocity = lastAgentVelocity;
        navAgent.SetPath(lastAgentPath);
        Debug.LogError("animRef.SetBool(\"isWalking\", !navAgent.isStopped);" + " state is  !navAgent.isStopped");


    }








    public void Movement(Vector3 movePoint) // moves towards a hit point (Player)
    {
        navAgent.SetDestination(movePoint);
    }



    private void OnTriggerEnter(Collider other)
    {//if you enter the range of an item you select it for interaction. CAREFUL - this means we can't have objects too close together
        if (player)
        {
            if (other.gameObject.GetComponent<GenericObject>() != null)
            {
                Debug.Log("entered the trigger range of object " + other.gameObject.GetComponent<GenericObject>().objectName);
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
                Debug.Log("left the trigger range of object " + other.gameObject.GetComponent<GenericObject>().objectName);
                other.gameObject.GetComponent<GenericObject>().Select(false);
            }
        }

    }


}
