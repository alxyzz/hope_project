using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventoryUIObject : MonoBehaviour
{
    public GenericObject containedObject;
    public Image itemBorder;
    public Image itemSlotBG;
    public Image itemImage;
    public Collider2D slotCollider;
   // public UnityEvent itemAction;
    //public TextMeshProUGUI itemName;



    private void Update()
    {
        if (containedObject == null)
        {
            itemImage.enabled = false;
            itemSlotBG.enabled = false;
            //itemName.enabled = false;
            itemBorder.enabled = false;
        }
        else
        {
            itemImage.enabled = true;
            itemSlotBG.enabled = true;
            //itemName.enabled = true;
            itemBorder.enabled = true;
        }
    }


    /// <summary>
    /// sets all necessary values/graphics to be officially in this slot
    /// </summary>
    /// 

    public void GotClicked()
    {
        Debug.Log(" INV BUTTON GOT CLICKED ");
        if (containedObject != null)
        {
            if (containedObject.inventoryUse_UnityEvent != null)
            {
                containedObject.inventoryUse_UnityEvent.Invoke();
            }
            
        }


    }
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
            //itemName.text = containedObject.objectName;
        }
        else
        {
            //itemName.text = "Unknown";
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
