using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider hopeSlider;
    public Canvas ingameMenu;
    public List<InventoryUIObject> inventorySlotList = new List<InventoryUIObject>();

    public Image hopeVisualizerImage;
    public Sprite hopeVis1, hopeVis2, hopeVis3, hopeVis4, hopeVis5, hopeVis6; //going from hopeful to hopeless.



    public void RefreshHopeVisualisation()
    {
        if (IsBetween(DataStorage.maxHope, 0, 25))
        {
            hopeVisualizerImage.sprite = hopeVis6;
        }
        else if (IsBetween(DataStorage.maxHope, 26, 50))
        {
            hopeVisualizerImage.sprite = hopeVis5;
        }
        else if (IsBetween(DataStorage.maxHope, 51, 75))
        {
            hopeVisualizerImage.sprite = hopeVis4;
        }
        else if (IsBetween(DataStorage.maxHope, 76, 100))
        {
            hopeVisualizerImage.sprite = hopeVis3;
        }
        else if (IsBetween(DataStorage.maxHope, 101, 125))
        {
            hopeVisualizerImage.sprite = hopeVis2;
        }
        else if (IsBetween(DataStorage.maxHope, 126, 130))
        {
            hopeVisualizerImage.sprite = hopeVis1;
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

    public void RefreshInventoryMenu()
    {

        for (int i = 0; i < DataStorage.objectsInInventory.Count; i++)
        {
            if (inventorySlotList[i] != null)
            {

            }
        }


    }


}
