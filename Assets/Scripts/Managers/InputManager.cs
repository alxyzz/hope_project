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

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

                GenericObject targetObject = m_hit.collider.GetComponent<GenericObject>();
                Entity targetEntity = m_hit.collider.GetComponent<Entity>();
                if (targetObject != null && DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject == targetObject)
                {
                    Debug.Log("hit object " + m_hit.transform.name);
                    //DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject = targetObject;
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
            Debug.Log("pressed space");
            //meth/crack/horse ketamine/LSD/PCP/weed
            if (DataStorage.GameManagerComponent.TripManagerComponent.tripStatus == 1)
            {
                Debug.Log("already sober so we will be taking a little chemical enjoyment");
                if (DataStorage.maxHope > 20)
                {
                    DataStorage.GameManagerComponent.TripManagerComponent.Trip();
                    DataStorage.maxHope -= 15;
                    Debug.Log("good trip");
                }
                else
                {
                    //addiction - hopeless. different hallucinations, bad trip?
                    DataStorage.GameManagerComponent.TripManagerComponent.BadTrip();
                    Debug.Log("bad trip");
                }

                DataStorage.timesUsedDrugs++;
            }
            


        }


    }




}
