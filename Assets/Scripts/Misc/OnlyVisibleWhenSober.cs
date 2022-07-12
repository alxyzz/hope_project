using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyVisibleWhenSober : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DataStorage.GameManagerComponent.TripComponent.soberOnlyGameObjects.Add(gameObject);
        
    }

    // Update is called once per frame
}
