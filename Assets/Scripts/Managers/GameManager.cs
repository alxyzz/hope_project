using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    


    //register various scripts here. just drag and drop in the inspector
    public CameraManager CameraManagerComponent;
    public InputManager InputManagerComponent;
    public MainMenuManager MainMenuManagerComponent;
    public StorylineManager StorylineManagerComponent;
    public UIManager UIManagerComponent;

    public DialogueManager CharacterInteractions;
    public ItemInteractionManager ItemInteractions;

    public Entity player;
    public GameObject pickupableObjects;

    private void Awake()
    {
        DataStorage.GameManagerComponent = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        DataStorage.Player = player;
        DataStorage.currentlyHeldObject = player.GetComponentInChildren<GenericObject>();
        // for loop to put pickupable objects in a list


        if(DataStorage.lastSceneName != null)
        {
            DataStorage.Player.transform.position = DataStorage.savedPlayerLoc;
            //later on, this is where we can do stuff like checking if there's a file with the current game state saved as a json (all kinds of booleans probably) and load it
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
