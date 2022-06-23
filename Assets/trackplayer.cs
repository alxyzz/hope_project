using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackplayer : MonoBehaviour
{
    private Transform playert;

    void Start()
    {
        playert = DataStorage.Player.transform;
    }
    void Update()
    {
        if (playert != null)
        {
            transform.LookAt(playert);
        }
        
    }
}
