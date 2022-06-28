using Fungus;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractionManager : MonoBehaviour
{
    public Flowchart fungusReference;
    [Space(10)]

    public GenericObject currentlySelectedObject;
    public GenericObject lastUsedObject;
    public Material SelectedObjectMaterial;


    public Sprite placeholderItemImage;



    private bool localCanBePutDown;
    //we need to reference all interactible objects with a decision attached so we can give them the decision
    //stage 1 tutorial
    public GenericObject obj_TV;
    public GenericObject obj_chips;




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

    public void PutInBackpack() // item displayed in inventory (wip)
    {
        Debug.Log("inventory - " + DataStorage.objectsInInventory.Count);
        if (DataStorage.objectsInInventory.Count < 3)
        {
            int count = DataStorage.objectsInInventory.Count;
            DataStorage.currentlyHeldObject.GetComponent<Renderer>().enabled = false;
            DataStorage.objectsInInventory[count] = lastUsedObject;
            DataStorage.GameManagerComponent.UIComponent.inventorySlotList[count].containedObject = lastUsedObject;
            DataStorage.GameManagerComponent.UIComponent.inventorySlotList[count].EquipItemHere();
        }
        else
        {
            Debug.LogWarning("Inventory is full :(");
        }
    }

    public List<GenericObject> startingPlayerItems = new();
    
    public void GivePlayerStartingItems()
    {
        foreach (GenericObject item in startingPlayerItems)
        {

        }


    }



    /// hardcoded object interactio

    //////BATHROOM ROOM START

    private bool checkifSober()
    {
        if (DataStorage.GameManagerComponent.TripComponent.tripStatus == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UsePuke()
    {//there's no puke object yet. also it disappears on being high


        fungusReference.ExecuteBlock("click_puke_sober_tutorial");



    }
    public void UseMirror()
    {
        //DataStorage.GameManagerComponent.UIManagerComponent.PopupMessagebox("Damn, this outfit looks good…<br>But not on me.”");
        if (checkifSober())
        {
            fungusReference.ExecuteBlock("click_mirror_sober_tutorial");
        }
        else
        {
            fungusReference.ExecuteBlock("click_mirror_high_tutorial");
        }


    }

    public void UseMagazines()
    {
        if (checkifSober())
        {
            fungusReference.ExecuteBlock("click_magazine_sober_tutorial");
        }
        else
        {
            fungusReference.ExecuteBlock("click_magazine_high_tutorial");
        }

    }
    public void UseToilet()
    {
        if (checkifSober())
        {
            fungusReference.ExecuteBlock("click_toilet_sober_tutorial");
        }
        else
        {
            fungusReference.ExecuteBlock("click_toilet_high_tutorial");
        }

    }

    public void UseBathtub()
    {
        if (checkifSober())
        {
            fungusReference.ExecuteBlock("click_bathtub_sober_tutorial");
        }
        else
        {
            fungusReference.ExecuteBlock("click_bathtub_high_tutorial");
        }

    }

    public void UseDoor()
    {
        if (checkifSober())
        {
            fungusReference.ExecuteBlock("click_door_sober_tutorial");
        }
        else
        {
            fungusReference.ExecuteBlock("click_door_high_tutorial");
        }


    }
    //////BATHROOM ROOM END

}
