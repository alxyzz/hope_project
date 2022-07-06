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

    private float desiredDistanceFromPlayer;
    private Camera mainCam;
    private void Start()
    {
        mainCam = Camera.main;//so we only call it once
        desiredDistanceFromPlayer = Vector3.Distance(transform.position,player.transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) > maximumHorizontalDistanceFromPlayer)
        {
            Vector3 b = Vector3.MoveTowards(transform.position, player.transform.position, movementStep);
            transform.position = new Vector3(b.x, transform.position.y, transform.position.z);
        }
        float dist = player.transform.position.z - mainCam.transform.position.z;
        if (Mathf.Abs(dist) != desiredDistanceFromPlayer)
        {
            Vector3 b = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + desiredDistanceFromPlayer - 0.2f), 0.03f); //magic number my beloved
            transform.position = new Vector3(transform.position.x, transform.position.y, b.z ); 
        }
    }
}
