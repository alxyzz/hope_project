using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// basically, I use DataStorage so I can call it, as a static function, from anywhere, to reference any of the instanced managers defined.
///instead of using a singleton.
/// </summary>
public static class DataStorage
{
    //registering stuff

    //entities
    public static Entity Player;
    public static int maxHope, currentHope;
    public static List<Entity> allLivingEntities = new List<Entity>();

    //objects
    public static List<Object> allObjects = new List<Object>();
    public static List<Object> allpickupableObjects = new List<Object>();
    //if you want to get list of items that cant be picked up
    //var results = allObjects.Where(i => !allpickupableObjects.Any(e => i.Contains(e)));


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

}
