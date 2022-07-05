using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DecisionManager;

public class DecisionUIElement : MonoBehaviour
{




    [HideInInspector]
    public List<Button> decisionButtons = new();


    public void ToggleVisibility(bool togg)
    {
        gameObject.SetActive(togg);
    }



    void ClearPreviousDecisions()
    {
        foreach (Button item in decisionButtons)
        {
            item.onClick = null;
            Text txty = item.GetComponentInChildren(typeof(Text), true) as Text;
            txty.text = "";

        }

    }

    public void UpdateSelf()
    {
        ClearPreviousDecisions();
        List<Decision> decs = DataStorage.GameManagerComponent.DecisionComponent.currentDecisions;

        for (int i = 0; i < decs.Count; i++)
        {
            decisionButtons[i].onClick = null;
            Text txty = decisionButtons[i].GetComponentInChildren(typeof(Text), true) as Text;
            txty.text = "";
        }


    }

}
