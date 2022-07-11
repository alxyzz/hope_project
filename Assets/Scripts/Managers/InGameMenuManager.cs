using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuManager : MonoBehaviour
{
    public GameObject setingsReference;
    public GameObject areYouSureReference;
    public GameObject pauseMenuReference;
    
    public void ResumeButton() // goes back to the game
    {
        pauseMenuReference.SetActive(false);
        setingsReference.SetActive(false);
        areYouSureReference.SetActive(false);
        Time.timeScale = 1;
    }
    public void SettingsButton() // audio settings ? if i have time
    {
        setingsReference.SetActive(true);
        pauseMenuReference.SetActive(false);
    }
    public void QuitButtonOne() // turns on prompt "are you sure?"
    {
        areYouSureReference.SetActive(true);
    }
    public void QuitButtonTwo() // quits
    {
        Application.Quit();
    }
}
