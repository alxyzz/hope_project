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
    public Entity player;

    public DecisionManager DecisionComponent;


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

        Vector3 spawnloc = new Vector3();
        switch (StartingRoomPicker)
        {
            case StartingPlayerRoom.Bathroom:
                spawnloc = StorylineComponent.bathroomEntryPoint.position;
                break;
            case StartingPlayerRoom.Bedroom:
                spawnloc = StorylineComponent.bedroomEntryPoint.position;
                break;
            default:
                break;
        }

        player.transform.position = spawnloc;

    }


    // Start is called before the first frame update
    void Start()
    {
        DataStorage.Player = player;
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
        
        


    }

    // Update is called once per frame
    void Update()
    {

    }
}
