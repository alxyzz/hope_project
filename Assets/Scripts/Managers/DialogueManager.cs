using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogueManager : MonoBehaviour
{

    public Flowchart selfFlowchart;

    // this is basically how we talk to NPCs. Fungus has it's own variables i believe to decide the dialogue so this is just so we can call these functions via a UnityEvent in the character's entity


    public void PositiveSelfTalk()
    {
        Debug.Log("we talked");
        selfFlowchart.ExecuteBlock("player clicked self");


    }

    public void TalkJulia()
    {




    }


    public void TalkMaria()
    {




    }
    public void TalkGeorge()
    {




    }


}
