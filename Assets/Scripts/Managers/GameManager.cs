using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{



    //register various scripts here. just drag and drop in the inspector
    public CameraManager CameraManagerComponent;
    public InputManager InputManagerComponent;
    public StorylineManager StorylineManagerComponent;
    public UIManager UIManagerComponent;
    public TripManager TripManagerComponent;
    public DialogueManager CharacterInteractions;
    public ItemInteractionManager ItemInteractions;

    public Entity player;
    public Transform pickupableObjects;


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
    }


    // Start is called before the first frame update
    void Start()
    {
        DataStorage.Player = player;
        DataStorage.currentlyHeldObject = player.GetComponentInChildren<GenericObject>();


        foreach (Transform pObject in pickupableObjects)
        {
            DataStorage.allpickupableObjects.Add(pObject.GetComponent<GenericObject>());
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
