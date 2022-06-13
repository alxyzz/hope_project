using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider hopeSlider;
    public Canvas ingameMenu;
    




    public void ToggleIngameMenu()
    {
        if (ingameMenu.gameObject.activeSelf)
        {
            Time.timeScale = 0;
            ingameMenu.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            ingameMenu.gameObject.SetActive(true);
        }


    }




}
