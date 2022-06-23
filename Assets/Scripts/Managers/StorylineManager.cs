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
    public bool usedMirror, usedMagazines, usedToilet, usedBathtub, usedDoor;
    public float elapsedTime;

    private void Update()
    {
        switch (CurrentLevel)
        {
            case "Act 1":
                if (CurrentRoom == "Bathroom")
                {
                    CheckIfSpiderAngry();
                }
                Debug.Log("test. StorylineManager detected Act 1 scene.");
                

                break;
            default:
                break;
        }
    }

    public void SpiderKickOut()
    {

        
        
    }



    public void CheckIfSpiderAngry()
    {

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= bathroomTimeUntilKickedOut)
        {
            DataStorage.GameManagerComponent.DialogueComponent.selfFlowchart.ExecuteBlock("spider_is_mad_lvl01");
        }

    }

}
