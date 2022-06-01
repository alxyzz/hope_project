using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update



    public void NewGame()
    {
        SceneManager.LoadScene("Act 1");
    }



    public void Continue()
    {
        //only one save slot (the last one)
    }


    public void Settings()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }














}
