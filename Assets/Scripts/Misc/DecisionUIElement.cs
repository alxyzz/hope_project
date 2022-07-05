using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DecisionManager;

public class DecisionUIElement : MonoBehaviour
{




    public List<Button> decisionButtons = new();


    public void ToggleVisibility(bool togg)
    {
        gameObject.SetActive(togg);
    }



    void ClearPreviousDecisions()
    {
        foreach (Button item in decisionButtons)
        {
            item.onClick.RemoveAllListeners(); //NOTE  - THIS DOES NOT REMOVE PERSISTENT EVENTS (added in inspector), only those added via code, so if yu want to do that for some reason do gameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            Text txty = item.GetComponentInChildren(typeof(Text), true) as Text;
            txty.text = "";

        }

    }

    public void UpdateSelf()
    {
        ClearPreviousDecisions();
        List<Decision> decs = DataStorage.GameManagerComponent.DecisionComponent.currentDecisions;
        decisionButtons.ForEach(n => n.gameObject.SetActive(true));
        for (int i = 0; i < decs.Count; i++)
        {

            UnityEngine.Events.UnityAction mbListener = new UnityEngine.Events.UnityAction(decs[i].targetMethodAction);
            
            decisionButtons[i].onClick.AddListener(mbListener);
            Text txty = decisionButtons[i].GetComponentInChildren(typeof(Text), true) as Text;
            txty.text = decs[i].decisionName;
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
