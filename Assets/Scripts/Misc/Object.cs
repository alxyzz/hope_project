using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Object : MonoBehaviour
{
    //same as entity but we're not going to be animating these (probably) or having a navigation agent

    public string objectName, description; 

    public bool pickupable;

    //usage
    public UnityEvent useFunction;//this can be changed to whatever you want to happen when you interact with this guy

    /// <summary>
    /// we will call Message("Interact") on whatever NPC or object we want to interact with later so this has the same name as the object function
    /// </summary>
    public void Interact()
    {

        useFunction.Invoke();
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
