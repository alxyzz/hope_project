using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    //same as entity but we're not going to be animating these (probably) or having a navigation agent

    public string objectName, description; 

    public bool pickupable;


    /// <summary>
    /// we will call Message("Interact") on whatever NPC or object we want to interact with later so this has the same name as the object function
    /// </summary>
    public void Interact()
    {


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
