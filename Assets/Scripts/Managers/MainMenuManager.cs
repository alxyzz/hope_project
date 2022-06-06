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
            }
            
        }
        else
        {
            if (ContinueButton != null)
            {
                ContinueButton.interactable = true;
            }
        }
    }

    public void NewGame()
    {
        DataStorage.WipeSave();
        SceneManager.LoadScene("Act 1");
    }



    public void Continue()
    {
        SceneManager.LoadScene(DataStorage.lastSceneName);
    }


    public void Settings()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }














}
