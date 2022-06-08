using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float maximumHorizontalDistanceFromPlayer;
    [SerializeField]
    private float movementStep;


    // Update is called once per frame
    void Update()
    {


        if (Mathf.Abs(player.transform.position.x - transform.position.x) > maximumHorizontalDistanceFromPlayer)
        {
            Vector3 b = Vector3.MoveTowards(transform.position, player.transform.position, movementStep);
            transform.position = new Vector3(b.x, transform.position.y, transform.position.z);
        }

    }



}
