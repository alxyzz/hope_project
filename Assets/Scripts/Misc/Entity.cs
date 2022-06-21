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
            if (GetComponent<Animator>() != null)
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
        if (entityName == "Cat")
        {
            Transform playertrans = DataStorage.Player.transform;
            //if x negative and z negative -> look upright
            //if x positive and z negative -> look upleft

            //if x positive and z positive -> look downleft
            //if x negative and z positive -> look downright
            //the following take precedence due to more specific conditions
            //if player is directly to the x+ but z is within 2f -> look left
            //if player is directly to the x- but z is within 2f -> look right
            // if player is directly to the z+ but x is within 2f -> look down
            //if player is directly to the z- but x is within 2f -> look up
            if (Vector3.Distance(playertrans.position, transform.position) > LookAnimation_LookRadius)
            {//not looking at anyone
                animRef.SetInteger("lookDir", 9);

            }

            else
            if (playertrans.position.x > transform.position.x && (Mathf.Abs(playertrans.position.z - transform.position.z) < LookAnimation_CardinalDirectionMaxAcceptableSidewaysDifference))
            {//look left done
                animRef.SetInteger("lookDir", 4);
            }
            else if (playertrans.position.x < transform.position.x && (Mathf.Abs(playertrans.position.z - transform.position.z) < LookAnimation_CardinalDirectionMaxAcceptableSidewaysDifference))
            {//look right done
                animRef.SetInteger("lookDir", 5);
            }
            else if (playertrans.position.z > transform.position.z && (Mathf.Abs(playertrans.position.x - transform.position.x) < LookAnimation_CardinalDirectionMaxAcceptableSidewaysDifference))
            {//look down done
                animRef.SetInteger("lookDir", 7);
            }
            else if (playertrans.position.z < transform.position.z && (Mathf.Abs(playertrans.position.x - transform.position.x) < LookAnimation_CardinalDirectionMaxAcceptableSidewaysDifference))
            {//look up done
                animRef.SetInteger("lookDir", 2);
            }
            else
            if (playertrans.position.x > transform.position.x && playertrans.position.z > transform.position.z)
            {//look downleft done
                animRef.SetInteger("lookDir", 6);
            }
            else if (playertrans.position.x < transform.position.x && playertrans.position.z > transform.position.z)
            {//look downright done
                animRef.SetInteger("lookDir", 8);
            }
            else if (playertrans.position.x < transform.position.x && playertrans.position.z < transform.position.z)
            {//look upright done
                animRef.SetInteger("lookDir", 3);
            }
            else if (playertrans.position.x > transform.position.x && playertrans.position.z < transform.position.z)
            {//look upleft done
                animRef.SetInteger("lookDir", 1);
            }







        }




    }

    public float LookAnimation_CardinalDirectionMaxAcceptableSidewaysDifference;
    public float LookAnimation_LookRadius;

    public bool IsBetween(double testValue, double bound1, double bound2)
    {
        return (testValue >= System.Math.Min(bound1, bound2) && testValue <= System.Math.Max(bound1, bound2));
    }
    public float CalculateAngle180_v3(Vector3 fromDir, Vector3 toDir)
    {
        float angle = Quaternion.FromToRotation(fromDir, toDir).eulerAngles.y;
        if (angle > 180) { return angle - 360f; }
        return angle;
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
        else if (entityName == "Cat")
        {

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
        else if (entityName == "Cat")
        {

        }



    }


}
