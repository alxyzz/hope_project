using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// basically, I use DataStorage so I can call it, as a static function, from anywhere, to reference any of the instanced managers defined.
///instead of using a singleton.
/// </summary>
public static class DataStorage
{
    //registering stuff

    //managers
    public static GameManager GameManagerComponent;










    //entities
    public static Entity Player;
    public static int maxHope = 100, currentHope = 100;
    public static bool isHigh;
    public static List<Entity> allLivingEntities = new List<Entity>();

    //objects
    public static GenericObject currentlyHeldObject; // object that player currently holds (the Grab child of Player GameObject)
    public static List<GenericObject> allObjects = new List<GenericObject>();
    public static List<GenericObject> allpickupableObjects = new List<GenericObject>();
    //if you want to get list of items that cant be picked up
    //var results = allObjects.Where(i => !allpickupableObjects.Any(e => i.Contains(e)));
    public static List<GenericObject> objectsInInventory = new List<GenericObject>(); //maximum 3 items i think? coz only 3 slots in the design UI
    public static List<Light> lightsInWorld = new List<Light>();
    //inventory UI objects are stored in UIManager


    //saving
    public static string lastSceneName;
    public static Vector3 savedPlayerLoc;

    //narrative booleans. can be used later for saving into a json along with player pos, etc
    //these are just for example
    public static bool
    hasMetFriends,
    hasUsedDrugs,
    hasChips,
    hasHasGivenChipsToFriends,
    hasInjectedPureMarijuanaOutOfDespair;


    //misc info for score and such
    public static int
    timesUsedDrugs,
    timesPetCat,
    timesRepetitiveDialogueChoices;



    //achievements
    public static bool
    hasSadEnding,
    hasBadEnding,
    hasGoodEnding,
    hasSecretEnding,
    hasSillyEnding,
    hasFoundEasterEgg,
    hasRippedMorbiusPoster;


    public static void WipeSave()
    {
        lastSceneName = null;
        savedPlayerLoc = new Vector3(0, 0, 0);

    }

    public static void MakeSave()
    {
        //serialize all info into a json

        //gotta look into how to serialize the fungus data
    }

    public static void LoadSave()
    {
        //deserialize all info from a json
    }

}
