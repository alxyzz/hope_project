using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DecisionManager : MonoBehaviour
{
    [HideInInspector]
    public List<Decision> currentDecisions = new();
    public DecisionUIElement DecisionUI; //this stores references to the necessary stuff
    [HideInInspector]
    public GameObject TargetObject;
    [Range(0.01f, 10f)]
    public float buttonTimeInterval;//basically the frequency of movement


    Camera maincam;
    public void ChangeTargetObject(GameObject go)
    {
        GenericObject genobj = go.GetComponent<GenericObject>();
        Entity entit = go.GetComponent<Entity>();
        if (genobj != null)
        {
            currentDecisions = genobj.Decisions;
        }
        else if (entit != null)
        {
            currentDecisions = entit.relatedDecisions;
        }
        
    }
    /*
     * how we do this:
     * first we add the decisions to the object
     * then we edit the static decision UI menu's stuff to fit the decisions
     * then we simply pop it up when it is required, no rescaling necessary because its the same object just different button effects and names
     */
    [System.Serializable]
    public class Decision
    {
        public string decisionName;
        public UnityEvent targetMethodAction;

    }

    public void EpicTestFunction()
    {

        Debug.LogError("lol. lmao.");

    }

    public void PopUp()
    {
        DataStorage.GameManagerComponent.InputComponent.IsThereAPopUp = true;
        DecisionUI.UpdateSelf();
        DecisionUI.ToggleVisibility(true);
        
    }

    public void Disappear()
    {
        DataStorage.GameManagerComponent.InputComponent.IsThereAPopUp = false;
        currentDecisions.Clear();
        DecisionUI.ToggleVisibility(false);
    }

    void Start()
    {
        maincam = Camera.main;
        DecisionUI.gameObject.SetActive(false);
    }
}
