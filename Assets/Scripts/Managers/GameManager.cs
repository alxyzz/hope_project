using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{



    //register various scripts here. just drag and drop in the inspector
    public CameraManager CameraComponent;
    public InputManager InputComponent;
    public StorylineManager StorylineComponent;
    public UIManager UIComponent;
    public TripManager TripComponent;
    public DialogueManager DialogueComponent;
    public ItemInteractionManager ItemComponent;
    public SoundManager SoundComponent;
    public InGameMenuManager MenuComponent;
    public Entity player;
    public int level = 1;
    public DecisionManager DecisionComponent;
    public DrugOverlayer drugOverlayer;

    public GameObject alreadyInInventoryParent; // parent object of items that are in inventory from the beginning

    public List<Transform> pickupableObjects = new(); //empty entries here without an actual reference will cause a null exception so just doublecheck this in the inspector 
    [Space(25)]
    public StartingPlayerRoom StartingRoomPicker;
    /// <summary>
    /// room in which the player starts
    /// </summary>
    public enum StartingPlayerRoom
    {
        Bathroom,
        Bedroom
    }



    public void BlackOut()
    {
        Debug.Log("Blackout...");
        ItemComponent.fungusReference.ExecuteBlock("fadeout_block"); 
        // teleport player 

    }





    public void ChangeHope(int amt)
    {
        DataStorage.currentHope += amt;

        UIComponent.RefreshHopeVisualisation();
    }

    private void Awake()
    {
        DataStorage.GameManagerComponent = this;

        if (DataStorage.lastSceneName != null)
        {
            DataStorage.lastSceneName = SceneManager.GetActiveScene().name;
            Debug.Log(DataStorage.lastSceneName + " is the current scene name");
            DataStorage.Player.transform.position = DataStorage.savedPlayerLoc;
            //later on, this is where we can do stuff like checking if there's a file with the current game state saved as a json (all kinds of booleans probably) and load it
        }


        //if ()
        //{

        //}
        //switch (StartingRoomPicker)
        //{
        //    case StartingPlayerRoom.Bathroom:
        //        spawnloc = StorylineComponent.bathroomEntryPoint.position;
        //        break;
        //    case StartingPlayerRoom.Bedroom:
        //        spawnloc = StorylineComponent.bedroomEntryPoint.position;
        //        break;
        //    default:
        //        break;
        //}
        DataStorage.Player = player;
        if (level == 2)
        {
            InputComponent.canUseDrugs = true;
        }
        //player.transform.position = spawnloc;

    }


    // Start is called before the first frame update
    void Start()
    {
        DataStorage.currentlyHeldObject = player.GetComponentInChildren<GenericObject>();
        if (pickupableObjects != null)
        {
            if (pickupableObjects.Count != 0)
            {
                foreach (Transform pObject in pickupableObjects)
                {
                    DataStorage.allpickupableObjects.Add(pObject.GetComponent<GenericObject>());
                }

            }

        }

        DataStorage.textIsOnScreen = false;
        
        MenuComponent.pauseMenuReference.SetActive(false);
        MenuComponent.setingsReference.SetActive(false);
        MenuComponent.areYouSureReference.SetActive(false);

        // equips starting items
        int i = 0;
        if (alreadyInInventoryParent != null)
        {
            foreach (GenericObject go in alreadyInInventoryParent.GetComponentsInChildren<GenericObject>())
            {
                UIComponent.inventorySlotList[i].EquipItemHere(go);
                i++;
            }
        }
        else
        {
            if (DataStorage.objectsInInventory.Count != 0)
            {
                foreach (GenericObject go in DataStorage.objectsInInventory)
                {
                    UIComponent.inventorySlotList[i].EquipItemHere(go);
                    i++;
                }
            }
        }

        TripComponent.ChangeHallucinatedObjectVisibilityStatus();

    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
