using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Entity player;
    // Start is called before the first frame update
    void Start()
    {
        DataStorage.Player = player;

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
