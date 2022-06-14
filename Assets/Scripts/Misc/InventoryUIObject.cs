using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIObject : MonoBehaviour
{
    public GenericObject containedObject;
    public Image itemImage;


    /// <summary>
    /// sets all necessary values/graphics to be officially in this slot
    /// </summary>
    public void EquipItemHere()
    {
        itemImage.sprite = containedObject.itemSprite;

    }
    /// <summary>
    /// just used to clear this, we assume dropping has been properly handled somewhere else.
    /// </summary>
    public void Unequip()
    {
        containedObject = null;

    }
}
