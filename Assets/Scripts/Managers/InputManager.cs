using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // mouse
    [HideInInspector]
    public Vector3 mousePosition;
    public Ray m_castPoint;
    public RaycastHit m_hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // finds mouse on screen, casts ray from screen to world, starts schmooving if ray touches anything other than the player itself
        if (DataStorage.GameManagerComponent.player.navAgent.isStopped == false)
        {
            mousePosition = Input.mousePosition;
            m_castPoint = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(m_castPoint, out m_hit, Mathf.Infinity))
            {
                if (!m_hit.collider.CompareTag("Player"))
                    DataStorage.Player.Movement(m_hit.point);
                //Debug.Log("we're moving yes");
            }
        }
        




        if (Input.GetKeyDown(KeyCode.Space))
        {
           //meth/crack/horse ketamine/LSD/PCP/weed
        }

        if (Input.GetKeyDown(KeyCode.E))
        {//interact with object
            DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject.Interact();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {//toggle movement on RMB
            Debug.Log("pressed RMB");
            if (DataStorage.GameManagerComponent.player.navAgent.isStopped)
            {
                DataStorage.GameManagerComponent.player.ResumeMovement();
            }
            else
            {
                DataStorage.GameManagerComponent.player.PauseMovement();
            }
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {//click on something
            m_castPoint = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(m_castPoint, out m_hit, Mathf.Infinity))
            {
                if (m_hit.collider.CompareTag("Interactible")) 
                {
                    //pick up/talk/etc
                }

            }
        }
    }


}
