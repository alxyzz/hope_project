using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionManager : MonoBehaviour
{
    public List<DecisionButton> dButtons = new();
    /// <summary>
    /// amount of space at which the button floats from the center of the popup per each move
    /// </summary>
    [Range(0.01f, 10f)]
    public float timeToDestination;
    public Transform centerPos;
    /// <summary>
    /// frequency of movement
    /// </summary>
    [Range(0.01f, 10f)]
    public float buttonTimeInterval;//basically the frequency of movement

    Camera maincam;
    public void ChangeTargetObject(GameObject go)
    {
        dButtons.Clear();
        Vector3 objectPosition = maincam.WorldToScreenPoint(go.transform.position);
        transform.position = new Vector3(objectPosition.x, objectPosition.y, 0);




    }


    public void PopUp()
    {
        foreach (DecisionButton item in dButtons)
        {
            item.Appear();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        maincam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
