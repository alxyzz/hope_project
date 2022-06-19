using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractionManager : MonoBehaviour
{
    public GenericObject currentlySelectedObject;
    public GenericObject lastUsedObject;
    public Material SelectedObjectMaterial;


    private bool localCanBePutDown;



    public void Grab()   // picks up object, puts its equivalent in Player's hand, disactivates the original object
    {
        if (DataStorage.allpickupableObjects.Contains(lastUsedObject) && lastUsedObject.pickupable) // checks if the right mesh is highlighted, also if player has picked anything else up
        {
            if (!DataStorage.currentlyHeldObject.GetComponent<Renderer>().enabled)
            {
                //Debug.Log("Grab is doing the thing");
                localCanBePutDown = lastUsedObject.canBePutDown;
                DataStorage.currentlyHeldObject.GetComponent<MeshFilter>().mesh = lastUsedObject.GetComponent<MeshFilter>().mesh;
                DataStorage.currentlyHeldObject.transform.localScale = lastUsedObject.transform.localScale;
                DataStorage.currentlyHeldObject.GetComponent<Renderer>().material = lastUsedObject.GetComponent<GenericObject>().originalMat;
                DataStorage.currentlyHeldObject.GetComponent<Renderer>().enabled = true;
                lastUsedObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else if (localCanBePutDown) // puts down IN THE SAME SPOT as it was picked up from
            {
                //Debug.Log("Grab is putting it down");
                DataStorage.currentlyHeldObject.GetComponent<Renderer>().enabled = false;
                lastUsedObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
    public void Test()
    {
        Debug.LogWarning("Testing events");
    }
    
    public void PutInBackpack() // item displayed in inventory (wip)
    {
        if (DataStorage.objectsInInventory.Count < 3)
        {
            int count = DataStorage.objectsInInventory.Count;
            DataStorage.currentlyHeldObject.GetComponent<Renderer>().enabled = false;
            DataStorage.objectsInInventory[count] = lastUsedObject;
        }
    }



    



}
