using System.Collections;
using System.Collections.Generic;
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
    public Transform bathroomEntryPoint, bedroomEntryPoint;
    

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
        DataStorage.Player.transform.position = bedroomEntryPoint.position;
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





    public void CheckIfSpiderAngry()
    {

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= bathroomTimeUntilKickedOut)
        {
            DataStorage.GameManagerComponent.DialogueComponent.selfFlowchart.ExecuteBlock("spider_is_mad_lvl01");
        }

    }

}
