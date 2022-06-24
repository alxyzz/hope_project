using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlightOnPlayerApproach : MonoBehaviour
{
    //so we don't have a collider and trigger on same object
    public GenericObject obj;


    private void OnTriggerEnter(Collider other)
    {
        Entity b = other.gameObject.GetComponent<Entity>();
        if (b != null)
        {
            if (b.player)
            {
                Debug.Log("entered the trigger range of object " + obj.objectName);
                obj.Highlight(true);
            }


        }

    }

    private void OnTriggerExit(Collider other)
    {

        Entity b = other.gameObject.GetComponent<Entity>();
        if (b != null)
        {
            Debug.Log("left the trigger range of object " + obj.objectName);
            obj.Highlight(false);
        }

    }
}
