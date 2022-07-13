using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkWhatDisabled : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        Debug.Log("i just got disabled. my name is " + this.name);
    }
}
