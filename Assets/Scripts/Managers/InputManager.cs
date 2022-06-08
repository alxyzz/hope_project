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
        mousePosition = Input.mousePosition;
        m_castPoint = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(m_castPoint, out m_hit, Mathf.Infinity))
        {
            if (!m_hit.collider.CompareTag("Player"))
            {
                DataStorage.Player.Movement(m_hit.point);
            }
            // todo: check whether mouse has hovered on an interactable object
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

                GenericObject targetObject = m_hit.collider.GetComponent<GenericObject>();
                Entity targetEntity = m_hit.collider.GetComponent<Entity>();
                if (targetObject != null)
                {
                    Debug.Log("hit object " + m_hit.transform.name);
                    DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject = targetObject;
                    DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject.Interact();
                }
                else if (targetEntity != null)
                {

                    targetEntity.Interact();
                }



            }
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


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //meth/crack/horse ketamine/LSD/PCP/weed

            if (DataStorage.maxHope > 20)
            {
                DataStorage.GameManagerComponent.VisualsManagerComponent.Trip();
                DataStorage.maxHope -= 15;
            }
            else
            {
                //addiction - hopeless. different hallucinations, bad trip?
                DataStorage.GameManagerComponent.VisualsManagerComponent.BadTrip();
            }

            DataStorage.timesUsedDrugs++;


        }


    }




}
