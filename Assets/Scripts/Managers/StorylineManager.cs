using UnityEngine;

public class StorylineManager : MonoBehaviour
{
    [HideInInspector]
    public int BathroomExaminedObjects;
    public int BathroomTargetExaminedObjects; //hit this number in previous variable to be able to see the inventory
    public float bathroomTimeUntilKickedOut;

    public string CurrentLevel;
    public string CurrentRoom;
    [HideInInspector]
    public bool usedMirror, usedMagazines, usedToilet, usedBathtub, usedDoor;
    [HideInInspector]
    public float elapsedTime;
    [HideInInspector]
    public bool bathroomLocked;
    //firstlevel points
    public Transform bathroomEntryPoint, bedroomEntryPoint;
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

        kitchenEntry = kitchenEntryG.transform.position;
        secondHallwayEntry = secondHallwayEntryG.transform.position;
        devRoomEntry = devRoomEntryG.transform.position;
        basementEntry = basementEntryG.transform.position;
        childhoodEntry = childhoodEntryG.transform.position;
    }
    private void Update()
    {
        switch (CurrentLevel)
        {
            case "Act 1":
                if (CurrentRoom == "Bathroom")
                {
                    CheckIfSpiderAngry();
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
        CurrentRoom = "Bedroom";
        bathroomLocked = true;

        CharacterController cc = DataStorage.Player.GetComponent<CharacterController>();

        cc.enabled = false; //yea the character controller does not like it if you change the position, you gotta turn it off and on again...
        DataStorage.Player.transform.position = bedroomEntryPoint.transform.position;
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


    public void CheckIfSpiderAngry()
    {

        //elapsedTime += Time.deltaTime;
        //if (elapsedTime >= bathroomTimeUntilKickedOut)
        //{
        //    DataStorage.GameManagerComponent.DialogueComponent.selfFlowchart.ExecuteBlock("spider_is_mad_lvl01");
        //}


        if (usedMirror && usedMagazines && usedToilet && usedBathtub && usedDoor)
        {
            showeringSpider.SetActive(true);
            DataStorage.GameManagerComponent.DialogueComponent.selfFlowchart.ExecuteBlock("ShowSpider");
            DataStorage.GameManagerComponent.UIComponent.hopeVisible = true;
            DataStorage.GameManagerComponent.UIComponent.backpackVisible = true;
        }
    }

}
