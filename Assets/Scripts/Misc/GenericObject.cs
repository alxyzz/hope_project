using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericObject : MonoBehaviour
{
    //same as entity but we're not going to be animating these (probably) or having a navigation agent

    public string objectName, description; 

    public bool pickupable;

    //usage
    public UnityEvent useFunction;//this can be changed to whatever you want to happen when you interact with this guy

    private bool hasHighlightedObject;
    /// <summary>
    /// we will call Message("Interact") on whatever NPC or object we want to interact with later so this has the same name as the object function
    /// </summary>
    public void Interact()
    {
        DataStorage.GameManagerComponent.ItemInteractions.lastUsedObject = this; //we store a reference of this item so we can do stuff like pick it up
        useFunction.Invoke();
    }

    public void Select(bool select) // highlights the selectable object (is kind of broken, either the trigger doesnt 
    {
        if (select && !hasHighlightedObject)
        {
            hasHighlightedObject = true;
            DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject = this;
            DataStorage.GameManagerComponent.ItemInteractions.previouslySelectedObjectMaterial = gameObject.GetComponent<Renderer>().material;
            gameObject.GetComponent<Renderer>().material = DataStorage.GameManagerComponent.ItemInteractions.SelectedObjectMaterial;
        }
        else
        {
            hasHighlightedObject = false;
            DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject = null;
            gameObject.GetComponent<Renderer>().material = DataStorage.GameManagerComponent.ItemInteractions.previouslySelectedObjectMaterial;
        }
        

    }

    public void Grab() // picks up object, puts its equivalent in Player's hand, disactivates the original object
    {
        if (DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject == this && !DataStorage.currentlyHeldObject.GetComponent<Renderer>().enabled) // checks if the right mesh is highlighted, also if player has picked anything else up
        {
            DataStorage.currentlyHeldObject.GetComponent<MeshFilter>().mesh = DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject.GetComponent<MeshFilter>().mesh;
            DataStorage.currentlyHeldObject.transform.localScale = DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject.transform.localScale;
            DataStorage.currentlyHeldObject.GetComponent<Renderer>().material = DataStorage.GameManagerComponent.ItemInteractions.previouslySelectedObjectMaterial;
            DataStorage.currentlyHeldObject.GetComponent<Renderer>().enabled = true;
            DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject.gameObject.SetActive(false);
        }
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
