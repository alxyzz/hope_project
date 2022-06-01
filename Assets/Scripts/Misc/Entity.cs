using UnityEngine;
using UnityEngine.AI;
using Fungus;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{

    public string entityName, description;
    
    public bool talks; //in case it does not want to talk
    public bool animating;//used for animation
    public bool wandering;//wether it periodically moves around in an area
    Vector3 startingPosition; //used to track starting position so we don't go too far from it while wandering

    public float moveDelay;//for random periodic movement if necessary
    public float moveDistance;//the amount it moves if it moves randomly 

    public float moveSpeed;

    
    //navigation
    NavMeshAgent NavAgent;
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


    // Update is called once per frame
    void Update()
    {

    }
}
