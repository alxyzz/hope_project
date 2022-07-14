using UnityEngine;
using Fungus;

public class StorylineManager : MonoBehaviour
{
    [HideInInspector]
    public int BathroomExaminedObjects;
    [HideInInspector]
    public int targetObjectExaminations = 5; //hit this number in previous variable to be able to see the inventory
    public float bathroomTimeUntilKickedOut;

    public string CurrentLevel;
    public string CurrentRoom;
    [HideInInspector]
    public bool usedMirror, usedMagazines, usedToilet, usedBathtub, usedDoor = false;
    [HideInInspector]
    public float elapsedTime;
    [HideInInspector]
    public bool bathroomLocked;
    //firstlevel points
    public Transform bathroomEntryPoint, bedroomEntryPoint, hallwayEntryPoint, kitchenEntryPoint;
    [Space(10)]
    public Transform secondLevelBlackoutSpot, firstLevelBlackoutSpot; //place where you respawn after blackout
    [Space(10)]
    public GameObject kitchenEntryG, secondHallwayEntryG, devRoomEntryG, basementEntryG, childhoodEntryG, storageEntryG;
    [HideInInspector]
    public Vector3 kitchenEntry, secondHallwayEntry, devRoomEntry, basementEntry, childhoodEntry, storageEntry;



    //kitchen//
    public GameObject kitchenSunrays;


    //kitchen end//


    private void Start()
    {
        //     public GameObject kitchenEntryG, secondHallwayEntryG, devRoomEntryG, basementEntryG, childhoodEntryG, storageEntryG;
        //public Vector3 kitchenEntry, secondHallwayEntry, devRoomEntry, basementEntry, childhoodEntry, storageEntry;
        if (DataStorage.GameManagerComponent.level == 2)
        {
            kitchenEntry = kitchenEntryG.transform.position;
            secondHallwayEntry = secondHallwayEntryG.transform.position;
            devRoomEntry = devRoomEntryG.transform.position;
            basementEntry = basementEntryG.transform.position;
            childhoodEntry = childhoodEntryG.transform.position;
        }
        
    }
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
            case "Act 2":
                DataStorage.GameManagerComponent.ItemComponent.fungusReference.ExecuteBlock("basement_prompt");
                break;
            default:
                break;
        }
    }

    public void SpiderKickOut()
    {
        Camera playerCamera = Camera.main;
        //it honestly feels kinda weird to go from the code -> to the flowchart -> just to invoke this function in the code
        SoundPlayer.PlaySound("door_sfx");
        CurrentRoom = "Bedroom";
        bathroomLocked = true;

        CharacterController cc = DataStorage.Player.GetComponent<CharacterController>();
        playerCamera.transform.parent = DataStorage.Player.transform;
        cc.enabled = false; //yea the character controller does not like it if you change the position, you gotta turn it off and on again...
        DataStorage.Player.transform.position = bedroomEntryPoint.transform.position;
        cc.enabled = true;
        playerCamera.transform.parent = null;
        
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


        if (usedMirror && usedMagazines && usedToilet && usedBathtub && usedDoor)
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
        if (DataStorage.GameManagerComponent.ItemComponent.CalculateWetherBathroomWasExaminedEnough())
        {
            Debug.Log("Bruh theres spider behind curtin???");
            showeringSpider.SetActive(true);
            DataStorage.GameManagerComponent.UIComponent.hopeVisible = true;
            DataStorage.GameManagerComponent.ItemComponent.fungusReference.ExecuteBlock("spider_showering_tutorial"); //this should wait a few seconds then run the method that kicks you out
        }

    }

}
