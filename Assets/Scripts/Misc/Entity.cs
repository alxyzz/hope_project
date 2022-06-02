using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{

    public string entityName, description;

    public bool talks; //in case it does not want to talk
    public bool animating;//used for animation

    public bool moving;//wether or not it moves around physically
    public float moveDelay;//for random periodic movement if necessary
    public float moveDistance;//the amount it moves if it moves randomly 

    public float moveSpeed;

    public NavMeshAgent navAgent;

    /// <summary>
    /// we will call Message("Interact") on whatever NPC or object we want to interact with later so this has the same name as the object function
    /// </summary>
    public void Interact()
    {


    }

    public void Movement(Vector3 movePoint) // moves towards a point in a set speed 
    {
        movePoint.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, movePoint, moveSpeed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 

    }
}
