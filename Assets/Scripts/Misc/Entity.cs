using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public System.Collections.Generic.List<DecisionButton> relatedDecisions = new();
    public string entityName, description;

    public bool player = false;
    public bool talks; //in case it does not want to talk
    public bool animating;//used for animation
   
    Vector3 startingPosition; //used to track starting position so we don't go too far from it while wandering

    public float moveDelay;//for random periodic movement if necessary
    public float moveDistance;//the amount it moves if it moves randomly 
    public bool lastMovementWasRight;
    public float moveSpeed;
    public Rigidbody rigidBody;

    Vector3 lastpos;


    Animator animRef;
    //navigation
    //public NavMeshAgent navAgent;
    Vector3 aiDestination; //the point where the AI will navigate to if 1. moving is true 2. it's not 0,0,0
    //for pausing and unpausing
    Vector3 lastAgentVelocity;
    //NavMeshPath lastAgentPath;
    //talk to
    public UnityEvent talkFunction;//this can be changed to whatever you want to happen when you interact with this little guy

    public float lookAnimation_CardinalDirectionMaxAcceptableSidewaysDifference;
    public float lookAnimation_LookRadius;

    /// <summary>
    /// we will call Message("Interact") on whatever NPC or object we want to interact with later so this has the same name as the object function
    /// </summary>
    public void Interact()
    {
        if (!player)
        {
            if (relatedDecisions.Count == 0)
            {
                DataStorage.GameManagerComponent.DecisionComponent.gameObject.SetActive(true);
                DataStorage.GameManagerComponent.DecisionComponent.ChangeTargetObject(gameObject);
                DataStorage.GameManagerComponent.DecisionComponent.PopUp();
                return;
            }
            if (talks)
            {
                if (talkFunction != null)
                {
                    talkFunction.Invoke();
                }
                
            }
            
        }

    }

    public void Start()
    {
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

    void Update()
    {
        HandleAnimation();
    }


    private void HandleAnimation()
    {
        if (!animating || animRef == null)
        {
            return;
        }
        if (player)
        {
            HandlePlayerAnimation();
        }
        if (entityName == "Cat")
        {
            HandleCatEyeTracking();
        }
    }


    private void HandlePlayerAnimation()
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

    private void HandleCatEyeTracking()
    {
        //1 2 3
        //4   5
        //6 7 8
        Transform playerTransform = DataStorage.Player.transform;
        //if x negative and z negative -> look upright
        //if x positive and z negative -> look upleft

        //if x positive and z positive -> look downleft
        //if x negative and z positive -> look downright
        //the following take precedence due to more specific conditions
        //if player is directly to the x+ but z is within 2f -> look left
        //if player is directly to the x- but z is within 2f -> look right
        // if player is directly to the z+ but x is within 2f -> look down
        //if player is directly to the z- but x is within 2f -> look up
        if (Vector3.Distance(playerTransform.position, transform.position) > lookAnimation_LookRadius)
        {//not looking at anyone
            animRef.SetInteger("lookDir", 9);
        }
        else
        if (playerTransform.position.x > transform.position.x && (Mathf.Abs(playerTransform.position.z - transform.position.z) < lookAnimation_CardinalDirectionMaxAcceptableSidewaysDifference))
        {//look left done
            animRef.SetInteger("lookDir", 4);
        }
        else if (playerTransform.position.x < transform.position.x && (Mathf.Abs(playerTransform.position.z - transform.position.z) < lookAnimation_CardinalDirectionMaxAcceptableSidewaysDifference))
        {//look right done
            animRef.SetInteger("lookDir", 5);
        }
        else if (playerTransform.position.z > transform.position.z && (Mathf.Abs(playerTransform.position.x - transform.position.x) < lookAnimation_CardinalDirectionMaxAcceptableSidewaysDifference))
        {//look down done
            animRef.SetInteger("lookDir", 7);
        }
        else if (playerTransform.position.z < transform.position.z && (Mathf.Abs(playerTransform.position.x - transform.position.x) < lookAnimation_CardinalDirectionMaxAcceptableSidewaysDifference))
        {//look up done
            animRef.SetInteger("lookDir", 2);
        }
        else
        if (playerTransform.position.x > transform.position.x && playerTransform.position.z > transform.position.z)
        {//look downleft done
            animRef.SetInteger("lookDir", 6);
        }
        else if (playerTransform.position.x < transform.position.x && playerTransform.position.z > transform.position.z)
        {//look downright done
            animRef.SetInteger("lookDir", 8);
        }
        else if (playerTransform.position.x < transform.position.x && playerTransform.position.z < transform.position.z)
        {//look upright done
            animRef.SetInteger("lookDir", 3);
        }
        else if (playerTransform.position.x > transform.position.x && playerTransform.position.z < transform.position.z)
        {//look upleft done
            animRef.SetInteger("lookDir", 1);
        }
    }

    //no need for this since we no longer use navmesh for movement
    //public void PauseMovement()
    //{

    //    Debug.Log("ran pausemovement");
    //    lastAgentPath = navAgent.path;
    //    lastAgentVelocity = navAgent.velocity;
    //    navAgent.velocity = Vector3.zero;
    //    navAgent.isStopped = true;

    //    animRef.SetBool("walkRight", false);
    //    animRef.SetBool("walkLeft", false);




    //}

    //public void ResumeMovement()
    //{
    //    Debug.Log("ran resumemovement");
    //    navAgent.isStopped = false;
    //    navAgent.velocity = lastAgentVelocity;
    //    navAgent.SetPath(lastAgentPath);
    //    Debug.LogError("animRef.SetBool(\"isWalking\", !navAgent.isStopped);" + " state is  !navAgent.isStopped");


    //}



    //public void Movement(Vector3 movePoint) // moves towards a hit point (Player)
    //{
    //    navAgent.SetDestination(movePoint);
    //}



    private void OnTriggerEnter(Collider other)
    {//if you enter the range of an item you select it for interaction. CAREFUL - this means we can't have objects too close together
        if (player)
        {
            if (other.gameObject.GetComponent<GenericObject>() != null)
            {
                Debug.Log("entered the trigger range of object " + other.gameObject.GetComponent<GenericObject>().objectName);
                //other.gameObject.GetComponent<GenericObject>().inRangeOfPlayer = true;
                other.gameObject.GetComponent<GenericObject>().Highlight(true);
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
                //other.gameObject.GetComponent<GenericObject>().inRangeOfPlayer = false;
                other.gameObject.GetComponent<GenericObject>().Highlight(false);
            }
        }
    }
}
