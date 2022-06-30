using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionManager : MonoBehaviour
{
    public List<DecisionButton> dButtons = new();
    public List<Rect> finalPositions = new();
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
    public float timeBeforeDisappearance;//basically the frequency of movement

    Camera maincam;
    public void ChangeTargetObject(GameObject go)
    {
        dButtons.Clear();
        Vector3 objectPosition = maincam.WorldToScreenPoint(go.transform.position);
        transform.position = new Vector3(objectPosition.x, objectPosition.y, 0);




    }


    public void PopUp()
    {
        int b = 0;
        foreach (DecisionButton item in dButtons)
        {
            item.transform.position = centerPos.position;
            item.gameObject.SetActive(true);
            item.Appear();
            item.finalPosition = finalPositions[b].position;
            b++;
        }

    }

    public void Disappear()
    {


    }

    IEnumerator delayedDisappear()
    {


        yield return new WaitForSecondsRealtime(timeBeforeDisappearance);
        foreach (DecisionButton item in dButtons)
        {
            item.Disappear();
        }           
        gameObject.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {
        maincam = Camera.main;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
