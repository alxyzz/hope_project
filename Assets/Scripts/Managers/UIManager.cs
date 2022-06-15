using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider hopeSlider;
    public Canvas ingameMenu;
    public List<InventoryUIObject> inventorySlotList = new List<InventoryUIObject>();
    public TextMeshProUGUI maxHopeText;
    public Image hopeVisualizerImage;
    public Sprite hopeVis1, hopeVis2, hopeVis3, hopeVis4, hopeVis5; //going from hopeful to hopeless.



    public void RefreshHopeVisualisation()
    {
        maxHopeText.text = DataStorage.maxHope.ToString() + "%";
        hopeSlider.value = DataStorage.maxHope;
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
