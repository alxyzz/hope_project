using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIObject : MonoBehaviour
{
    public GenericObject containedObject;
    public Image itemBorder;
    public Image itemImage;
    public TextMeshProUGUI itemName;



    private void Update()
    {
        if (containedObject == null)
        {
            itemImage.enabled = false;
            itemName.enabled = false;
            itemBorder.enabled = false;
        }
        else
        {
            itemImage.enabled = true;
            itemName.enabled = true;
            itemBorder.enabled = true;
        }
    }


    /// <summary>
    /// sets all necessary values/graphics to be officially in this slot
    /// </summary>
    public void EquipItemHere(GenericObject target = null)
    {
        if (target != null)
        {
            containedObject = target;
        }
        if (containedObject.itemSprite != null)
        {
            itemImage.sprite = containedObject.itemSprite;
        }
        if (containedObject.objectName != null || containedObject.objectName != "")
        {
            itemName.text = containedObject.objectName;
        }
        else
        {
            itemName.text = "Unknown";
        }
        

    }
    /// <summary>
    /// just used to clear this, we assume dropping has been properly handled somewhere else.
    /// </summary>
    public void Unequip()
    {
        containedObject = null;

    }
}
