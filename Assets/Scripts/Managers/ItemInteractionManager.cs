using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractionManager : MonoBehaviour
{
    public GenericObject currentlySelectedObject;
    public GenericObject lastUsedObject;
    public Material SelectedObjectMaterial;




    public void Grab(bool canBePutDown) // picks up object, puts its equivalent in Player's hand, disactivates the original object
    {
        if (currentlySelectedObject == this) // checks if the right mesh is highlighted, also if player has picked anything else up
        {
            if (!DataStorage.currentlyHeldObject.GetComponent<Renderer>().enabled)
            {
                DataStorage.currentlyHeldObject.GetComponent<MeshFilter>().mesh = currentlySelectedObject.GetComponent<MeshFilter>().mesh;
                DataStorage.currentlyHeldObject.transform.localScale = currentlySelectedObject.transform.localScale;
                DataStorage.currentlyHeldObject.GetComponent<Renderer>().material = previouslySelectedObjectMaterial;
                DataStorage.currentlyHeldObject.GetComponent<Renderer>().enabled = true;
                currentlySelectedObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else if (canBePutDown) // puts down IN THE SAME SPOT as it was picked up from
            {
                DataStorage.currentlyHeldObject.GetComponent<Renderer>().enabled = false;
                currentlySelectedObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }








}
