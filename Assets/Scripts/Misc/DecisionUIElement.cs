using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static DecisionManager;

public class DecisionUIElement : MonoBehaviour
{

    public GameObject backgroundUIObject;


    public List<Button> decisionButtons = new();


    public void ToggleVisibility(bool togg)
    {
        DataStorage.GameManagerComponent.InputComponent.IsThereAPopUp = togg;
        gameObject.SetActive(togg);
        backgroundUIObject.SetActive(true);//this deactivates on click. its just to cancel the dialogue
    }

    [HideInInspector]
    public List<UnityEvent> buttonUnityEventList = new();//dirty but dont have time to look too deeply into this

    public void DirtyEventRunner(int index)
    {
        try
        {
            buttonUnityEventList[index].Invoke();
        }
        catch (System.Exception)
        {
            Debug.LogError("DirtyEventRunner messed up in DecisionUIElement.cs. buttonEvents[" + index + "] was probably null.");
            throw;
        }
        
    }

    void ClearPreviousDecisions()
    {
        foreach (Button item in decisionButtons)
        {


            item.onClick = new Button.ButtonClickedEvent();
            //item.onClick.RemoveAllListeners(); //NOTE  - THIS DOES NOT REMOVE PERSISTENT EVENTS (added in inspector), only those added via code, so if yu want to do that for some reason do gameObject.GetComponent<Button>().onClick = new     Button.ButtonClickedEvent();
            Text txty = item.GetComponentInChildren(typeof(Text), true) as Text;
            txty.text = "";

        }

    }

    public void SecEpicTestFunction()
    {

        Debug.LogError("lol. lmao.");

    }

    public void UpdateSelf()
    {
        ClearPreviousDecisions();
        List<Decision> decisionsOfCurrentClickedObject = DataStorage.GameManagerComponent.DecisionComponent.currentDecisions;
        //decisionButtons.ForEach(n => n.gameObject.SetActive(true));
        for (int i = 0; i < decisionsOfCurrentClickedObject.Count; i++)
        {// go through the amount of decisions, assign same amount of buttons, refresh button name and add the decision's respective UnityEvent to the onClick() of the button. ideally. this shit keeps messing up. im going to PR for now
            decisionButtons[i].gameObject.SetActive(true);
            decisionButtons[i].onClick = new Button.ButtonClickedEvent();//wipes persistent listeners
            if (decisionsOfCurrentClickedObject[i] == null)
            {
                Debug.Log("decisionsForClickedobject[i] is null!!!!!!!!! AAAAAAAAAAAAAAAA");
            }
            buttonUnityEventList[i] = decisionsOfCurrentClickedObject[i].targetMethodAction;

            void test() { DirtyEventRunner(i); }
            //UnityEditor.Events.UnityEventTools.AddPersistentListener(decisionButtons[i].onClick, test);

            //decisionButtons[i].onClick.AddPersistentListener();
            //decisionButtons[i].onClick.AddListener(() => decisionsForClickedobject[i].targetMethodAction.Invoke());
            decisionButtons[i].onClick.AddListener(() => test());
            Text txty = decisionButtons[i].GetComponentInChildren(typeof(Text), true) as Text;

            txty.text = decisionsOfCurrentClickedObject[i].decisionName;
            Debug.Log("adding listener to button " + txty.text + " \n decisionButtons[i].onClick.AddListener(() => decs[i].targetMethodAction.Invoke());   \n" + decisionsOfCurrentClickedObject[i].targetMethodAction + " is the target method action");
        }

        foreach (Button item in decisionButtons)
        {
            Text targetText = item.GetComponentInChildren(typeof(Text), true) as Text;
            if (targetText.text == "")
            {
                item.gameObject.SetActive(false);
            }
        }
    }

}
