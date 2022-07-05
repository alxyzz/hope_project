using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static DecisionManager;

public class GenericObject : MonoBehaviour
{
    [SerializeField]
    public DecisionInitializationObject[] decisionsToInitialize;
    [HideInInspector]
    public System.Collections.Generic.List<Decision> Decisions = new();
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
    [HideInInspector]
    public bool inRangeOfPlayer;//wether we are close enough to click 


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

    private bool isHighlighted;












    [System.Serializable]
    public class DecisionInitializationObject
    {
        [SerializeField]
        public string dName;
        public UnityEvent targetMethod;
    }


   















    private void Start()
    {
        originalMat = GetComponent<Renderer>().material;
        foreach (DecisionInitializationObject item in decisionsToInitialize)
        {

            Decision d = new Decision();
            
            d.decisionName = item.dName;
            
            
            d.targetMethodAction = item.targetMethod;
            Decisions.Add(d);

            Debug.Log("initialized decision \"" + item.dName + "\" and added it to the decisions list"); 
        }

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
        if (CheckAndShowDecisions())//if there are decisions related to this, we will not interact the usual way
        {
            return;
        }
        CheckAndRunUseEvent();//otherwise just run the typical interaction, like for simple hope modifiers
    }


    private void CheckAndRunUseEvent()
    {
        if (inworldUse_UnityEvent != null)
        {
            Debug.Log("we just clicked " + objectName);
            if (!usedForHope)
            {
                Debug.Log("used " + objectName + " for hope. modifier was " + hopeModifierOnInteraction.ToString());
                DataStorage.GameManagerComponent.ChangeHope(hopeModifierOnInteraction);
                usedForHope = true;
            }
            if (inworldUse_UnityEvent != null)
            {
                inworldUse_UnityEvent.Invoke();
            }
            
        }

    }
    private bool CheckAndShowDecisions()
    {
        if (Decisions.Count > 0)
        {
            DataStorage.GameManagerComponent.DecisionComponent.TargetObject = this.gameObject;
            DataStorage.GameManagerComponent.DecisionComponent.currentDecisions.Clear();
            foreach (Decision item in Decisions)
            {//we loop through em and pop em up
                DataStorage.GameManagerComponent.DecisionComponent.currentDecisions.Add(item);
            }
            DataStorage.GameManagerComponent.DecisionComponent.PopUp();

            return true;
        }
        else
        {
            return false;
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

    public void Highlight(bool select) // highlights the selectable object
    {

        if (select)
        {
            isHighlighted = true;
            gameObject.GetComponent<Renderer>().material = DataStorage.GameManagerComponent.ItemComponent.SelectedObjectMaterial;
        }
        else
        {
            isHighlighted = false;
            gameObject.GetComponent<Renderer>().material = originalMat;
        }
    }


   
}
