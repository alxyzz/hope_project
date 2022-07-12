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
        if (DataStorage.objectsInInventory.Count < 6)
        {
            int count = DataStorage.objectsInInventory.Count;
            DataStorage.objectsInInventory.Add(lastUsedObject);
            DataStorage.GameManagerComponent.UIComponent.inventorySlotList[count].EquipItemHere(lastUsedObject);
            lastUsedObject.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Inventory is full :(");
        }
    }


    public void DropItem(GenericObject b)
    {


        b.gameObject.SetActive(true);
        b.transform.position = DataStorage.Player.transform.position;
        DataStorage.objectsInInventory.Remove(b);


    }


    public void DeleteItem(GenericObject b)
    {

        DataStorage.objectsInInventory.Remove(b);

    }

    public List<GenericObject> startingPlayerItems = new();

    public void GivePlayerStartingItems()
    {
        foreach (GenericObject item in startingPlayerItems)
        {

        }


    }



    /// hardcoded object interaction





    //////////ITEMS////////////////////////////////////////////////
    ///




    public void UseSkateboard()
    {


        fungusReference.ExecuteBlock("click_skateboard_tutorial");


    }

    public void UsePhone()
    {

        if (checkifSober())
        {
            fungusReference.ExecuteBlock("click_phone_sober_tutorial");
        }
        else
        {
            fungusReference.ExecuteBlock("click_phone_high_tutorial");
        }

    }



    public void UseCandyJar()
    {
        fungusReference.ExecuteBlock("click_candyjar_tutorial");
    }



    public void UseID()
    {

        if (checkifSober())
        {
            fungusReference.ExecuteBlock("click_ID_tutorial");
        }
        else
        {
            fungusReference.ExecuteBlock("click_id_high_tutorial");
        }


    }



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


    //usedMirror, usedMagazines, usedToilet, usedBathtub, usedDoor
    public void UsePuke()
    {//there's no puke object yet. also it disappears on being high

        DataStorage.GameManagerComponent.StorylineComponent.ShowSpider();
        fungusReference.ExecuteBlock("click_puke_sober_tutorial");


    }
    public void UseMirror()
    {
        DataStorage.GameManagerComponent.StorylineComponent.usedMirror = true;
        DataStorage.GameManagerComponent.StorylineComponent.ShowSpider();
        //DataStorage.GameManagerComponent.UIManagerComponent.PopupMessagebox("Damn, this outfit looks good�<br>But not on me.�");
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
        DataStorage.GameManagerComponent.StorylineComponent.usedMagazines = true;
        DataStorage.GameManagerComponent.StorylineComponent.ShowSpider();
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
        DataStorage.GameManagerComponent.StorylineComponent.usedToilet = true;
        DataStorage.GameManagerComponent.StorylineComponent.ShowSpider();
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
        DataStorage.GameManagerComponent.StorylineComponent.usedBathtub = true;
        DataStorage.GameManagerComponent.StorylineComponent.ShowSpider();
        if (checkifSober())
        {
            fungusReference.ExecuteBlock("click_bathtub_sober_tutorial");
        }
        else
        {
            fungusReference.ExecuteBlock("click_bathtub_high_tutorial");
        }

    }

    public void UseDoorBathroom()
    {
        SoundPlayer.PlaySound("test_whip");
        DataStorage.GameManagerComponent.StorylineComponent.usedDoor = true;
        DataStorage.GameManagerComponent.StorylineComponent.ShowSpider();
        fungusReference.ExecuteBlock("click_door_sober_tutorial"); // same text in both



    }

    public bool CalculateWetherBathroomWasExaminedEnough()
    {
        //DataStorage.GameManagerComponent.StorylineComponent.BathroomExaminedObjects;
        int b = 0;
        //usedMirror, usedMagazines, usedToilet, usedBathtub, usedDoor


        if (DataStorage.GameManagerComponent.StorylineComponent.usedMirror)
        {
            b++;
        }
        if (DataStorage.GameManagerComponent.StorylineComponent.usedMagazines || DataStorage.isHigh)  //because thhe high bathrooom has less stuff
        {
            b++;
        }
        if (DataStorage.GameManagerComponent.StorylineComponent.usedToilet)
        {
            b++;
        }
        if (DataStorage.GameManagerComponent.StorylineComponent.usedBathtub)
        {
            b++;
        }
        if (DataStorage.GameManagerComponent.StorylineComponent.usedDoor)
        {
            b++;
        }
        if (b >= DataStorage.GameManagerComponent.StorylineComponent.targetObjectExaminations)
        {
            return true;
        }
        else
        {
            return false;
        }
    }




    //////BATHROOM ROOM END





    public bool bathroomisLocked; //affects wether you can enter the bathroom

    ////// bedroom
    ///

    public void UseDoorBedroomToBathroom()
    {
        SoundPlayer.PlaySound("test_whip");
        if (true)
        {

        }
        if (checkifSober())
        {
            fungusReference.ExecuteBlock("click_door_sober_tutorial");
        }
        else
        {
            fungusReference.ExecuteBlock("click_door_high_tutorial");
        }


    }
















    public void UseBedroomSkateboard()
    {
        if (checkifSober())
        {
            fungusReference.ExecuteBlock("click_skateboard_sober_bedroom");
        }
        else
        {
            fungusReference.ExecuteBlock("click_skateboard_high_tutorial");
        }

    }
    public void UseBedroomLaptop()
    {
        if (checkifSober())
        {
            fungusReference.ExecuteBlock("click_laptop_sober_bedroom");
        }
        else
        {
            fungusReference.ExecuteBlock("click_laptop_high_bedroom");
        }

    }

    // ///////////////// bedroom end

    // CORRIDOR / HALLWAY

    public void UsePhotos()
    {
        fungusReference.ExecuteBlock("click_photos_hallway");
    }
    public void Gundorb()
    {
        fungusReference.ExecuteBlock("click_gundorb");
    }

    // //////////////////// HALLWAY END

// kitchen



    public void UseKitchenChips(GameObject theChipsReference)
    {

        fungusReference.ExecuteBlock("chips_selection_kitchen");

        theChipsReference.SetActive(false);

    }




    ///////////// KITCHEN
    ///




    ///navigation///
    ///sure we could have just 1 block for each individual location but this way we can add triggers when entering certain rooms from others
    public void doorHallwayToKitchen()
    {
        //fungusReference.ExecuteBlock("fridge_sober");

        DataStorage.Player.PlayerTeleport(DataStorage.GameManagerComponent.StorylineComponent.kitchenEntry);

        if (!wentToKitchen)
        {
            fungusReference.ExecuteBlock("hallway_kitchen");
            wentToKitchen = true;
        }
    }
    private bool wentToKitchen;
    public void doorKitchenToHallway()
    {
        //fungusReference.ExecuteBlock("fridge_sober");
        DataStorage.Player.PlayerTeleport(DataStorage.GameManagerComponent.StorylineComponent.secondHallwayEntry);
    }

    public void doorHallwayToStorage()
    {
        //fungusReference.ExecuteBlock("fridge_sober");
        DataStorage.Player.PlayerTeleport(DataStorage.GameManagerComponent.StorylineComponent.storageEntry);
    }
    public void doorStorageToHallway()
    {
        //fungusReference.ExecuteBlock("fridge_sober");
        DataStorage.Player.PlayerTeleport(DataStorage.GameManagerComponent.StorylineComponent.secondHallwayEntry);
    }

    public void doorHallwayToBasement()
    {
        //fungusReference.ExecuteBlock("fridge_sober");
        DataStorage.Player.PlayerTeleport(DataStorage.GameManagerComponent.StorylineComponent.basementEntry);
    }

    public void doorBasementToHallway()
    {
        //fungusReference.ExecuteBlock("fridge_sober");
        DataStorage.Player.PlayerTeleport(DataStorage.GameManagerComponent.StorylineComponent.secondHallwayEntry);
    }

    public void doorBasementToDevRoom()
    {
        //fungusReference.ExecuteBlock("fridge_sober");
        DataStorage.Player.PlayerTeleport(DataStorage.GameManagerComponent.StorylineComponent.devRoomEntry);
    }

    public void useKitchenFridge()
    {

        if (checkifSober())
        {
            fungusReference.ExecuteBlock("fridge_sober");
        }
        else
        {
            fungusReference.ExecuteBlock("fridge_high");
        }

    }

    public void useKitchenTelevision()
    {

        if (checkifSober())
        {
            fungusReference.ExecuteBlock("television_sober");
        }
        else
        {
            fungusReference.ExecuteBlock("television_high");
        }

    }
    public void useKitchenPoopDoor()
    {

        if (checkifSober())
        {
            fungusReference.ExecuteBlock("door_poop_sober");
        }
        else
        {
            fungusReference.ExecuteBlock("door_poop_high");
        }

    }

    public void useKitchenSexDoor()
    {

        if (checkifSober())
        {
            fungusReference.ExecuteBlock("door_sex_sober");
        }
        else
        {
            fungusReference.ExecuteBlock("door_sex_high");
        }

    }
    public void useKitchenGardenDoor()
    {

        if (checkifSober())
        {
            fungusReference.ExecuteBlock("door_to_garden_sober");
        }
        else
        {
            fungusReference.ExecuteBlock("door_to_garden_high");
        }

    }

    public void useKitchenBeerPong()
    {

        if (checkifSober())
        {
            fungusReference.ExecuteBlock("click_laptop_sober_bedroom");
        }
        else
        {
            fungusReference.ExecuteBlock("click_laptop_high_bedroom");
        }

    }
    public void useKitchenPenguin()
    {

        if (checkifSober())
        {
            //fungusReference.ExecuteBlock("click_laptop_sober_bedroom");
        }
        else
        {
            fungusReference.ExecuteBlock("penguin_msg_1");
        }

    }
}
