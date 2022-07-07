using Fungus;
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
    public List<UnityEvent> buttonUnityEventList = new(); // the root of all problems

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
    public void RunDecisionEvent(string fungus_block)//this should include the object identifier
    {
        fungusReference.ExecuteBlock(fungus_block);
    }
    


    public Flowchart fungusReference;


    public void UpdateSelf()
    {
        ClearPreviousDecisions();
        List<Decision> declist = DataStorage.GameManagerComponent.DecisionComponent.currentDecisions;
        Debug.LogWarning("decisions count is " + declist.Count); // works
        if (declist != null) // works
        {
            for (int i = 0; i < declist.Count; i++)
            {
                decisionButtons[i].onClick.RemoveAllListeners();
                decisionButtons[i].gameObject.SetActive(true);
                Text targetText = decisionButtons[i].GetComponentInChildren(typeof(Text), true) as Text;
                decisionButtons[i].onClick.RemoveAllListeners();
                decisionButtons[i].onClick.AddListener(() => RunDecisionEvent(declist[i].decisionName));
                targetText.text = declist[i].decisionName.Replace(declist[i].objectIdentification, string.Empty); ;
                
            }
        }
        else
        {
            Debug.LogWarning("the entire list of decisions is null idk");
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
