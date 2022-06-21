using UnityEngine;
using UnityEngine.Events;

public class GenericObject : MonoBehaviour
{
    //same as entity but we're not going to be animating these (probably) or having a navigation agent


    public string objectName, description;
    [HideInInspector]
    public Material originalMat;
    public bool singleUseInWorld;
    [HideInInspector]
    public bool usedInWorld;
    public bool singleUseInInventory;
    [HideInInspector]
    public bool usedInInventory;
    public bool pickupable;
    public bool canBePutDown; // after being picked up
    public Sprite itemSprite;

    public bool CheckIfInventory(){if (DataStorage.objectsInInventory.Contains(this)) return true; else return false;}

    //usage. these are stored in ItemInteractions
    public UnityEvent useInWorld;//this can be changed to whatever you want to happen when you interact with this guy
    public UnityEvent useInInventory;//this can be changed to whatever you want to happen when you use this stuff in the inventory. if any.

    private bool hasHighlightedObject;
    /// <summary>
    /// means usage in the world space
    /// </summary>
    public void Interact()
    {
        if (singleUseInWorld && usedInWorld)
        {
            return;
        }
        DataStorage.GameManagerComponent.ItemInteractions.lastUsedObject = this; //we store a reference of this item so we can do stuff like pick it up
        if (useInWorld != null)
        {
            useInWorld.Invoke();
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
        DataStorage.GameManagerComponent.ItemInteractions.lastUsedObject = this; //we store a reference of this item so we can do stuff like pick it up
        if (useInInventory != null)
        {
            useInInventory.Invoke();
        }


    }

    public void Select(bool select) // highlights the selectable object
    {

        if (select && !hasHighlightedObject)
        {
            hasHighlightedObject = true;
            DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject = this;
            gameObject.GetComponent<Renderer>().material = DataStorage.GameManagerComponent.ItemInteractions.SelectedObjectMaterial;
        }
        else
        {
            hasHighlightedObject = false;
            DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject = null;
            gameObject.GetComponent<Renderer>().material = originalMat;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        originalMat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
