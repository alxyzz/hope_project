using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRegistration : MonoBehaviour
{
    public Light componentLight;
    // Start is called before the first frame update
    void Start()
    {
        DataStorage.lightsInWorld.Add(componentLight);
    }


}
