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
        mousePosition = Input.mousePosition;
        m_castPoint = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(m_castPoint, out m_hit, Mathf.Infinity))
        {
            if (!m_hit.collider.CompareTag("Player"))
            DataStorage.Player.Movement(m_hit.point);
        }




        if (Input.GetKeyDown(KeyCode.Space))
        {
           //meth/crack/horse ketamine/LSD/PCP/weed
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DataStorage.GameManagerComponent.ItemInteractions.currentlySelectedObject.Interact();
        }
    }
}
