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

    public void TalkBook()
    {
        selfFlowchart.ExecuteBlock("Clicked on book");
    }


    //LIVING ROOM
    public int timesTalked;
    //public void TalkFriendsCollective()
    //{
    //    switch (timesTalked)
    //    {
    //        case 1:
    //            selfFlowchart.ExecuteBlock("livingRoom_friends_talk1");
    //            break;
    //        case 2:
    //            selfFlowchart.ExecuteBlock("livingRoom_friends_talk2");
    //            break;
    //        case 3:
    //            selfFlowchart.ExecuteBlock("livingRoom_friends_talk3");
    //            break;
    //        case 4:
    //            selfFlowchart.ExecuteBlock("livingRoom_friends_talk4");
    //            break;
    //        case 5:
    //            selfFlowchart.ExecuteBlock("livingRoom_friends_talk5");
    //            break;
    //        default:
    //            selfFlowchart.ExecuteBlock("livingRoom_friends_talk_6+");
    //            break;
    //    }
        
    //}

    //public bool TalkFriendsKitchen()
    //{

   // }
    public bool enteredKitchen;
    public void MonologueEnterKitchen()
    {
        if (!enteredKitchen)
        {
            selfFlowchart.ExecuteBlock("enter_kitchen");
            enteredKitchen = true;
        }
    }

    public void PetCatKitchen()
    {

        selfFlowchart.ExecuteBlock("kitchen_petcat");
    }

    public void ClickWindowKitchen()
    {

        selfFlowchart.ExecuteBlock("kitchen_window");
        DataStorage.GameManagerComponent.StorylineComponent.kitchenSunrays.SetActive(true);
        DataStorage.GameManagerComponent.InputComponent.CharacterControllerReference.canMove = false;
        DataStorage.Player.playerClosedEyes = true;
        StartCoroutine("RelaxByWindow");


    }
    IEnumerator RelaxByWindow()
    {
        yield return new WaitForSecondsRealtime(3f);
        DataStorage.GameManagerComponent.StorylineComponent.kitchenSunrays.SetActive(false);
        DataStorage.GameManagerComponent.InputComponent.CharacterControllerReference.canMove = true;
        DataStorage.Player.playerClosedEyes = false;
    }
    // ////////////// HALLWAY 2 LEVEL ////////////

    public void TalkLilOlive()
    {

    }

    // ////////////// DEVS ///////////////////////

    public void TalkAndrea()
    {
        selfFlowchart.ExecuteBlock("developers_andrea");
    }
    public void TalkFrieda()
    {
        selfFlowchart.ExecuteBlock("developers_frieda");
    }
    public void TalkDiego()
    {
        selfFlowchart.ExecuteBlock("developers_diego");

    }
    public void TalkRita()
    {
        selfFlowchart.ExecuteBlock("developers_rita");
    }
    public void TalkTimur()
    {
        selfFlowchart.ExecuteBlock("developers_timur");
    }
    public void TalkAlex()
    {
        selfFlowchart.ExecuteBlock("developers_alex");
    }

    // 
}
