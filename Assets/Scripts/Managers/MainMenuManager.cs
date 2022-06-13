using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MainMenuManager : MonoBehaviour
{

    public Button ContinueButton, StartButton, SettingsButton, ExitButton;



    // Start is called before the first frame update
    private void Start() 
    {
        HideContinueButtonIfNoSave();


    }


    void HideContinueButtonIfNoSave()
    {
        if (DataStorage.lastSceneName == null)
        {
            if (ContinueButton != null)
            {
                ContinueButton.interactable = false;
                ContinueButton.gameObject.SetActive(false);
            }
            
        }
        else
        {
            if (ContinueButton != null)
            {
                ContinueButton.interactable = true;
                ContinueButton.gameObject.SetActive(true);
            }
        }
    }

    public void NewGame()
    {
        Debug.Log("clciked newgame");
        DataStorage.WipeSave();
        SceneManager.LoadScene("Act 1");
    }



    public void Continue()
    {
        Debug.Log("clciked continue");
        SceneManager.LoadScene(DataStorage.lastSceneName);
    }


    public void Settings()
    {
        Debug.Log("clciked settings");
    }
    public void Exit()
    {
        Debug.Log("clciked exit. this doesnt do anything in editor");
        Application.Quit();
    }














}
