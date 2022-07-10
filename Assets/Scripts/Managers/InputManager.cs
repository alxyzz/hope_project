using System.Collections;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    // mouse
    [HideInInspector]
    public Vector3 mousePosition;
    public RaycastHit m_hit;
    [HideInInspector]
    public bool IsThereAPopUp;
    [HideInInspector]
    public bool canUseDrugs = false;
    [HideInInspector]
    public bool clicked = false;

    [SerializeField]
    public float minimumDistanceToTalkToPeople;

    public CharacterController_New CharacterControllerReference;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ClicksForOneFrame());
            ///Debug.Log("raw input - mouse LMB detected");
            if (IsThereAPopUp)
            {
              //  Debug.Log("theres a popup that blocks clicks. click ignored");
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out m_hit, 100f))
            {
                if (m_hit.transform != null)
                {

                    GenericObject targetObject = m_hit.transform.GetComponent<GenericObject>();
                    Entity targetEntity = m_hit.transform.GetComponent<Entity>();

                    if (targetObject != null)
                    {
                        if (targetObject.isHighlighted) // if the player is in ranges
                        {
                            targetObject.Interact();
                        }
                    }
                    else if (targetEntity != null)
                    {
                        if (Vector3.Distance(DataStorage.Player.transform.position, targetEntity.transform.position) <= minimumDistanceToTalkToPeople)
                        {
                            targetEntity.Interact();
                        }
                    }
                    else
                    {
                        Debug.Log("hit non-genericobject non-entity. Object name -" + m_hit.collider.name);
                    }



                }
            }
        }





       


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("pressed space");
            //meth/crack/horse ketamine/LSD/PCP/weed
            if (!canUseDrugs)
            {
                return;
            }
            if (DataStorage.maxHope > 0)
            {
                DataStorage.GameManagerComponent.TripComponent.GetHigh();
                DataStorage.maxHope -= 20;
                if (DataStorage.currentHope > DataStorage.maxHope)
                {
                    DataStorage.currentHope = DataStorage.maxHope;
                }
                DataStorage.GameManagerComponent.UIComponent.RefreshHopeVisualisation();
            }
            else
            {
                //addiction - hopeless. different hallucinations, bad trip?
                //DataStorage.GameManagerComponent.TripManagerComponent.BadTrip();
                //Debug.Log("bad trip");
            }
            DataStorage.timesUsedDrugs++;
        }


        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("pressed j debug sober button");
            //meth/crack/horse ketamine/LSD/PCP/weed

            Debug.Log("already sober so we will be taking a little chemical enjoyment");

            DataStorage.GameManagerComponent.TripComponent.GetLow();





        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("pressed ESC to pop up ingame menu");




        }





        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    switch (testint)
        //    {
        //        case 0:
        //            DataStorage.Player.StopPlayerStance();
        //            testint++;
        //            break;
        //        case 1:
        //            DataStorage.Player.PlayerAnimCloseEyes();
        //            testint++;
        //            break;
        //        case 2:
        //            DataStorage.Player.PlayerAnimSitting();
        //            testint++;
        //            break;
        //        case 3:
        //            DataStorage.Player.PlayerAnimBrowsePhone();
        //            testint = 0;
        //            break;
        //    }




        //}

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Debug.Log("hit L");
        //    DataStorage.GameManagerComponent.DialogueComponent.selfFlowchart.ExecuteBlock("create a dialogue");


        //}

        //    PlayerAnimBrowsePhone()
        //{
        //        if (!player) { Debug.Log("called player stance on wrong entity"); return; }
        //        doingStance = true;
        //        animRef.SetInteger("customStanceStatus", 3);
        //    }

        //    public void PlayerAnimSitting()
        //    {
        //        if (!player) { Debug.Log("called player stance on wrong entity"); return; }
        //        doingStance = true;
        //        animRef.SetInteger("customStanceStatus", 2);
        //    }

        //    public void PlayerAnimCloseEyes()
        //    {
        //        if (!player) { Debug.Log("called player stance on wrong entity"); return; }
        //        doingStance = true;
        //        animRef.SetInteger("customStanceStatus", 1);
        //    }

        //    public void StopPlayerStance()
    }
    
    private IEnumerator ClicksForOneFrame()
    {
        clicked = true;
        yield return null;
        clicked = false;
        yield return null;
    }
}
