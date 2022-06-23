using UnityEngine;
using UnityEngine.Events;

public class GenericObject : MonoBehaviour
{
    //same as entity but we're not going to be animating these (probably) or having a navigation agent
    [HideInInspector]
    public bool usedInWorld; //wether it has already been used
    [HideInInspector]
    public bool usedForHope; //wether it has already been used for hope gain/loss 
    [HideInInspector]
    public Material originalMat;
    [HideInInspector]
    public bool usedInInventory; //wether it has already been used in inventory
    public bool visibleHigh, visibleSober;
    public string objectName, description;
    public Sprite itemSprite; //as seen in inventory



    [Space(10)]
    public bool singleUseInWorld;
    public bool singleUseInInventory;
    [Space(5)]
    public bool pickupable;
    public bool canBePutDown; // after being picked up
    [Space(10)]
    public int hopeModifierOnInteraction = 0; //ok. so some objects just straight up don't have decisions linked to them and thus just affect your hope in some cases, while others pop up a decisionmaking UI and let you decide what to do. this variable is for the former case only
    [Space(10)]
    //usage. these are stored in ItemInteractions
    public UnityEvent inworldUse_UnityEvent;//this can be changed to whatever you want to happen when you interact with this guy
    public UnityEvent inventoryUse_UnityEvent;//this can be changed to whatever you want to happen when you use this stuff in the inventory. if any.

    private bool hasHighlightedObject;



    private void Start()
    {
        originalMat = GetComponent<Renderer>().material;
        
    }




    public bool CheckIfInventory() { if (DataStorage.objectsInInventory.Contains(this)) return true; else return false; }


    /// <summary>
    /// means usage in the world space
    /// </summary>
    public void Interact()
    {
        if (singleUseInWorld && usedInWorld)
        {
            return;
        }
        DataStorage.GameManagerComponent.ItemComponent.lastUsedObject = this; //we store a reference of this item so we can do stuff like pick it up
        if (inworldUse_UnityEvent != null)
        {
            if (!usedForHope)
            {
                Debug.Log("used " + objectName + " for hope. modifier was " + hopeModifierOnInteraction.ToString());
                DataStorage.GameManagerComponent.ChangeHope(hopeModifierOnInteraction);
                usedForHope = true;
            }
            inworldUse_UnityEvent.Invoke();
        }
    }

    /// <summary>
    /// means usage in inventory
    /// </summary>
    public void Use()
    {
        if (singleUseInInventory && usedInInventory)
        {
            return;
        }
        DataStorage.GameManagerComponent.ItemComponent.lastUsedObject = this; //we store a reference of this item so we can do stuff like pick it up
        if (inventoryUse_UnityEvent != null)
        {
            inventoryUse_UnityEvent.Invoke();
        }


    }

    public void Select(bool select) // highlights the selectable object
    {

        if (select && !hasHighlightedObject)
        {
            hasHighlightedObject = true;
            DataStorage.GameManagerComponent.ItemComponent.currentlySelectedObject = this;
            gameObject.GetComponent<Renderer>().material = DataStorage.GameManagerComponent.ItemComponent.SelectedObjectMaterial;
        }
        else
        {
            hasHighlightedObject = false;
            DataStorage.GameManagerComponent.ItemComponent.currentlySelectedObject = null;
            gameObject.GetComponent<Renderer>().material = originalMat;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
