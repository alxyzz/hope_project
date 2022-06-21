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

    /// hardcoded object interactions





    //////BATHROOM ROOM START




    public void UseMirror()
    {
        DataStorage.GameManagerComponent.UIManagerComponent.TalkToSelf("Damn, this outfit looks good…<br>But not on me.”");


    }

    public void UseMagazines()
    {
        DataStorage.GameManagerComponent.UIManagerComponent.TalkToSelf("Another boring day in shit town. They should’ve renamed this town years ago.<br>Lonelytown, fucking dumbass town,… *sigh*  whatever.”");
    }
    public void UseToilet()
    {
        DataStorage.GameManagerComponent.UIManagerComponent.TalkToSelf("Eww, that hasn’t been cleaned in sometime. Looks like someone didn’t flush…");
    }

    public void UseBathtub()
    {
        DataStorage.GameManagerComponent.UIManagerComponent.TalkToSelf("I could really use a bath. I haven’t been home in days.");
    }

    public void UseDoor()
    {
        if (DataStorage.GameManagerComponent.StorylineManagerComponent.BathroomExaminedObjects == DataStorage.GameManagerComponent.StorylineManagerComponent.BathroomTargetExaminedObjects)
        {
            //we leave

        }
        else
        {
            DataStorage.GameManagerComponent.UIManagerComponent.TalkToSelf("I don’t feel like leaving yet.");
        }
        
    }
    //////BATHROOM ROOM END

}
