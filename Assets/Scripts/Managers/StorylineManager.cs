using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorylineManager : MonoBehaviour
{
    [HideInInspector]
    public int BathroomExaminedObjects;
    public int BathroomTargetExaminedObjects; //hit this number in previous variable to be able to see the inventory

    public string CurrentLevel;
    public bool usedMirror, usedMagazines, usedToilet, usedBathtub, usedDoor;
    

    private void Update()
    {
        switch (CurrentLevel)
        {
            case "BathroomFirstEntry":
                
                break;
            default:
                break;
        }
    }

}
