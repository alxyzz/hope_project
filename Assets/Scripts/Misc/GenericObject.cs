using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericObject : MonoBehaviour
{
    //same as entity but we're not going to be animating these (probably) or having a navigation agent

    public string objectName, description;
    public Material originalMat;
    public bool pickupable;

    //usage
    public UnityEvent useFunction;//this can be changed to whatever you want to happen when you interact with this guy


    



    public void Interact()
    {
        DataStorage.GameManagerComponent.ItemInteractions.lastUsedObject = this; //we store a reference of this item so we can do stuff like pick it up
        useFunction.Invoke();
    }

    public void Select(bool select)
    {
        if (select)
        {
            Debug.Log("selected object");
            DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject = this;
            gameObject.GetComponent<Renderer>().material = DataStorage.GameManagerComponent.ItemInteractions.SelectedObjectMaterial;
        }
        else
        {
            Debug.Log("unselected object");
            DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject = null;
            gameObject.GetComponent<Renderer>().material = originalMat;
        }
        

    }

    // Start is called before the first frame update
    void Start()
    {
        originalMat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
