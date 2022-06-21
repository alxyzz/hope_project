using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public bool goesToNextScene;
    public Transform Destination;

    /// <summary>
    /// Door, aka portal to next level
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // we can integrate a little screen fadeout or sound effect or something that would tell the player that they went through the door
            if (goesToNextScene)
            {
                Invoke(nameof(LoadNextScene), 1f);
            }
            else
            {
                GotoDestination();
            }
            
        }
    }


    public void GotoDestination()
    {
        Debug.LogWarning("Teleporting through door.");
        DataStorage.Player.transform.position = Destination.position;
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
