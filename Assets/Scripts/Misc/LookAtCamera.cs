using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    //check where we just moved compared to last location
    private SpriteRenderer sprenderer;
    private float xloc;
    // Update is called once per frame


    private void Start()
    {
        sprenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 5f, transform.eulerAngles.z);
        //if (transform.position.x > xloc)
        //{//moved left
        //    sprenderer.flipX = false;
        //}
        //else if (transform.position.x < xloc)
        //{//moved right
        //    sprenderer.flipX = true;
        //}



        xloc = transform.position.x;
    }
}
