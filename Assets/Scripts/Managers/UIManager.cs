using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider hopeSlider;
    public Canvas ingameMenu;
    public Canvas UICanvas;
    public List<InventoryUIObject> inventorySlotList = new List<InventoryUIObject>();
    public TextMeshProUGUI maxHopeText;
    public Image hopeVisualizerImage;

    public Sprite hopeVis1, hopeVis2, hopeVis3, hopeVis4, hopeVis5; //going from hopeful to hopeless.
    /// <summary>
    /// maximum amount of messages that can be on the screen at the same time
    /// </summary>
    public int messageMaxStackedAmount;
    /// <summary>
    /// the distance messages appear from the player
    /// </summary>
    public float messageVerticalOffsetFromPlayer;
    /// <summary>
    /// time until a message fades
    /// </summary>

    public Sprite s_reflection, s_friendspics; //expand as required, these are the images for the below thing
    public Image popupImageObject; //image which we will change the sprite of when player looks in mirror/photo on wall etc



    //UI visiblity states
    [HideInInspector]
    public bool backpackVisible = false, hopeVisible = false;
    public GameObject invParent, hopeParent;


    private void Start()
    {
        RefreshHopeVisualisation();
        RefreshBackpackVisibility();
    }

    public void RefreshHopeVisualisation()
    {//Current Hope<br>100%<br Max Hope <br>100%

        if (!hopeVisible)
        {
            hopeParent.SetActive(false);
            return;
        }
        else
        {
            hopeParent.SetActive(true);
        }
        maxHopeText.text = "Current Hope<br>"+ DataStorage.currentHope.ToString() +"%<br>Max Hope<br>"+ DataStorage.maxHope.ToString()+"%";
        hopeSlider.value = DataStorage.currentHope;
        if (IsBetween(DataStorage.maxHope, 0, 20))
        {
            hopeVisualizerImage.sprite = hopeVis5;
        }
        else if (IsBetween(DataStorage.maxHope, 20, 40))
        {
            hopeVisualizerImage.sprite = hopeVis4;
        }
        else if (IsBetween(DataStorage.maxHope, 40, 60))
        {
            hopeVisualizerImage.sprite = hopeVis3;
        }
        else if (IsBetween(DataStorage.maxHope, 60, 80))
        {
            hopeVisualizerImage.sprite = hopeVis2;
        }
        else if (IsBetween(DataStorage.maxHope, 80, 100))
        {
            hopeVisualizerImage.sprite = hopeVis1;
        }
    }

    public void RefreshBackpackVisibility()
    {
        if (backpackVisible)
        {
            invParent.SetActive(true);
        }
        else
        {
            invParent.SetActive(false);
        }

    }

    public bool IsBetween(double testValue, double bound1, double bound2)
    {
        if (bound1 > bound2)
            return testValue >= bound2 && testValue <= bound1;
        return testValue >= bound1 && testValue <= bound2;
    }

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

    public void PopUpImage(Sprite whichImage)
    {//for stuff like looking in the mirror or examining a picture on the wall
        //show up pic
        //pic is anchored in the center
        //hide if it gets clicked again
        popupImageObject.gameObject.SetActive(true);
        popupImageObject.sprite = whichImage;
        DataStorage.GameManagerComponent.InputComponent.IsThereAPopUp = true;

    }
    public void PopDownImage() // hides (maybe we should change it to fade in / out instead?)
    {
        popupImageObject.gameObject.SetActive(false);
        DataStorage.GameManagerComponent.InputComponent.IsThereAPopUp = false;
    }
}
