using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class StorylineManager : MonoBehaviour
{
    [HideInInspector]
    public int BathroomExaminedObjects;
    public int BathroomTargetExaminedObjects; //hit this number in previous variable to be able to see the inventory
    public float bathroomTimeUntilKickedOut;

    public string CurrentLevel;
    public string CurrentRoom;
    [HideInInspector]
    public bool usedMirror, usedMagazines, usedToilet, usedBathtub, usedDoor = false;
    [HideInInspector]
    public float elapsedTime;
    [HideInInspector]
    public bool bathroomLocked;
    public Transform bathroomEntryPoint, bedroomEntryPoint, hallwayEntryPoint, kitchenEntryPoint;


    //kitchen//
    public GameObject kitchenSunrays;


    //kitchen end//

    private void Update()
    {
        switch (CurrentLevel)
        {
            case "Act 1":
                if (CurrentRoom == "Bathroom")
                {
                    //CheckIfSpiderAngry();
                }
                Debug.Log("test. StorylineManager detected bathroom scene.");
                

                break;
            default:
                break;
        }
    }

    public void SpiderKickOut()
    {
        //it honestly feels kinda weird to go from the code -> to the flowchart -> just to invoke this function in the code
        SoundPlayer.PlaySound("door_sfx");
        CurrentRoom = "Bedroom";
        bathroomLocked = true;

        CharacterController cc = DataStorage.Player.GetComponent<CharacterController>();

        cc.enabled = false; //yea the character controller does not like it if you change the position, you gotta turn it off and on again...
        DataStorage.Player.transform.position = bedroomEntryPoint.transform.position;
        cc.enabled = true;
        Camera playerCamera = Camera.main;
        playerCamera.transform.position = new Vector3(DataStorage.Player.transform.position.x, playerCamera.transform.position.y, playerCamera.transform.position.z);
    }
    public void GoToHallway()
    {
        SoundPlayer.PlaySound("door_sfx");
        CurrentRoom = "Hallway";
        CharacterController cc = DataStorage.Player.GetComponent<CharacterController>();
        cc.enabled = false; 
        DataStorage.Player.transform.position = hallwayEntryPoint.transform.position;
        cc.enabled = true;
        Camera playerCamera = Camera.main;
        playerCamera.transform.position = new Vector3(DataStorage.Player.transform.position.x, playerCamera.transform.position.y, playerCamera.transform.position.z);
    }
    public void GoToKitchen()
    {

        SoundPlayer.PlaySound("door_sfx");
        CurrentRoom = "Kitchen";
        CharacterController cc = DataStorage.Player.GetComponent<CharacterController>();
        cc.enabled = false;
        DataStorage.Player.transform.position = kitchenEntryPoint.transform.position;
        cc.enabled = true;
        Camera playerCamera = Camera.main;
        playerCamera.transform.position = new Vector3(DataStorage.Player.transform.position.x, playerCamera.transform.position.y, playerCamera.transform.position.z);
    }
    public void AllowDrugUse(bool togg)
    {
        DataStorage.GameManagerComponent.InputComponent.canUseDrugs = togg;
    }

    //public void ChangeRoom(string room)
    //{
    //    switch (string)
    //    {
    //        case "Bathroom":
    //            break;
    //        case "Bedroom"
    //        break;


    //        default:
    //            break;
    //    }



    //}


    public GameObject showeringSpider;


    public bool IsSpiderAngry()
    {

        //elapsedTime += Time.deltaTime;
        //if (elapsedTime >= bathroomTimeUntilKickedOut)
        //{
        //    DataStorage.GameManagerComponent.DialogueComponent.selfFlowchart.ExecuteBlock("spider_is_mad_lvl01");
        //}

        
        if (usedMirror && usedMagazines && usedToilet &&  usedBathtub && usedDoor)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ShowSpider()
    {
        if (IsSpiderAngry())
        {
            Debug.Log("Bruh theres spider behind curtin???");
            showeringSpider.SetActive(true);
            DataStorage.GameManagerComponent.UIComponent.hopeVisible = true;
            DataStorage.GameManagerComponent.ItemComponent.fungusReference.ExecuteBlock("spider_showering_tutorial");
        }

    }

}
