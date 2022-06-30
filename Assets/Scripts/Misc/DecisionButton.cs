using System.Collections;
using UnityEngine;

public class DecisionButton : MonoBehaviour
{

    //so basically this is both the button itself and the action. theyre the same thing. we add them to the desired object/entity gameobject, up to 4, add it to the list of the object/entity, and then pass it on to dialoguemanager on use
    //this means the previous way to interact is gone, we don't run unityEvents from the interactible unless there's no decisions

    float timeToDestination;
    public string dName; //the name/description of the action


    public TMPro.TextMeshProUGUI nameTextObject; //this turns to dname

    [HideInInspector]
    public Vector3 finalPosition;
    [HideInInspector]
    float desiredMinimumDistanceToFinalPosition;

    // Start is called before the first frame update
    void Start()
    {
        //import settings from decision manager
        timeToDestination = DataStorage.GameManagerComponent.DecisionComponent.timeToDestination;
        finalPosition = DataStorage.GameManagerComponent.DecisionComponent.centerPos.position;
        nameTextObject.text = dName;
    }

    private Transform oldParent;
    public void Appear()
    {
        oldParent = transform.parent;
        transform.SetParent(DataStorage.GameManagerComponent.DecisionComponent.transform);
        transform.position = DataStorage.GameManagerComponent.DecisionComponent.centerPos.position;
    }



    public void Disappear()
    {//simply cleans up the button from the display and reparents it

        transform.SetParent(oldParent);
        gameObject.SetActive(false);
        

    }

    private Vector3 velocity = Vector3.zero;
    private void Update()
    {//no performance issues, we just set this to inactive this when it's no longer relevant anyways
        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref velocity, timeToDestination);
    }


}
