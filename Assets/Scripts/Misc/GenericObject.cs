using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static DecisionManager;

public class GenericObject : MonoBehaviour
{
    //[SerializeField]
    //private List<string> decisionStrings = new();
    //[SerializeField]
    //private string objectIdentifyingString;

    [HideInInspector]
    public System.Collections.Generic.List<Decision> contextualDecisions = new();
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

    public bool isHighlighted;




    private bool isSprite;






    private void Start()
    {
        if (GetComponent<Renderer>() != null)
        {
            originalMat = GetComponent<Renderer>().material;
        }
        else
        {
            if (GetComponent<SpriteRenderer>() == true)
            {
                isSprite = true;
            }
        }
        
        AddStringPrefix();
    }

    void AddStringPrefix()
    {
        //List<string> str = new List<string>();
        //foreach (string stuff in decisionStrings)
        //{
        //    string b = objectIdentifyingString + stuff;
        //    str.Add(b);
        //}
        //decisionStrings.Clear();
        //decisionStrings.AddRange(str);


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
        //if (CheckAndShowDecisions())//if there are decisions related to this, we will not interact the usual way
        //{
        //    return;
        //}
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
                try
                {
                    inworldUse_UnityEvent.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.LogError("exception |" + ex.Message + " |at checkanduserunevent genericobject with name " + objectName);
                    throw;
                }
                
            }
            
        }

    }
    //private bool CheckAndShowDecisions()
    //{
    //    if (contextualDecisions.Count > 0)
    //    {
    //        Debug.Log("we are popping up");
    //        DataStorage.GameManagerComponent.DecisionComponent.TargetObject = this.gameObject;
    //        DataStorage.GameManagerComponent.DecisionComponent.currentDecisions.Clear();
    //        DataStorage.GameManagerComponent.DecisionComponent.currentDecisions.AddRange(contextualDecisions);

    //        DataStorage.GameManagerComponent.DecisionComponent.PopUp();

    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }


    //}

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

        if (gameObject.GetComponent<Renderer>() != null)
        {
            if (select)
            {
                isHighlighted = true;
                if (!isSprite) gameObject.GetComponent<Renderer>().material = DataStorage.GameManagerComponent.ItemComponent.SelectedObjectMaterial;
            }
            else
            {
                isHighlighted = false;
                if (!isSprite) gameObject.GetComponent<Renderer>().material = originalMat;
            }
        }
        else
        {
            if (select)
            {
                HighlightChildren(select);
            }
            else
            {
                HighlightChildren(select);
            }
        }
        

        
    }
    private List<Material> childMatList;
    private List<Transform> children;
    private void HighlightChildren(bool select)
    {
        if (children.Count == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                children.Insert(i,transform.GetChild(i));
            }
        }
        if (select)
        {
            for (int i = 0; i < children.Count; i++)
            {
                Transform target = children[i];
                if (target.GetComponent<Renderer>() != null)
                {
                    childMatList[i] = target.GetComponent<Renderer>().material;
                    isHighlighted = true;
                    target.GetComponent<Renderer>().material = DataStorage.GameManagerComponent.ItemComponent.SelectedObjectMaterial;
                }

            }
        }
        else
        {//deselect
            for (int i = 0; i < children.Count; i++)
            {
                Transform target = children[i];
                if (target.GetComponent<Renderer>() != null)
                {
                    childMatList[i] = transform.GetChild(i).GetComponent<Renderer>().material;

                    isHighlighted = false;
                    target.GetComponent<Renderer>().material = childMatList[i];
                    
                }
                
            }
            childMatList.Clear(); //clears child matlist when done
        }
       


    }

   
}
