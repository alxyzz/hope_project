using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMessageScript : MonoBehaviour
{
    [HideInInspector]
    public float fadeTime;
    [HideInInspector]
    public int movedUpMaxAmount;
    [HideInInspector]
    public int movedUpAmount;
    Vector3? targetPosition = null;
    [HideInInspector]
    public float slideSpeed; //this is modified on spawn

    public TMPro.TextMeshProUGUI TextRef;


    /// <summary>
    /// this runs instead of Start() on pooled objects
    /// </summary>
    public void OnObjectSpawn()
    {

    }

    /// <summary>
    /// resize to content size
    /// </summary>
    public void ResizeToContent()
    {
        
    }


    IEnumerator timedDisappearance()
    {

        yield return new WaitForSecondsRealtime(fadeTime);
        Debug.Log("disappearing messagebox.");
        Disappear();
    }

    /// <summary>
    /// make way for the newest message
    /// </summary>
    public void MakeWay()
    {
        RectTransform b = (RectTransform)transform;
        targetPosition = new Vector3(transform.position.x, transform.position.y + 2 + b.rect.height, transform.position.z) ;
        //movedUpAmount++;
        //if (movedUpAmount >= movedUpMaxAmount)
        //{
        //    Disappear();
        //}
    }

    public void Disappear()
    {

        DataStorage.GameManagerComponent.UIManagerComponent.messageQueue.Remove(this);
        targetPosition = null;
        gameObject.SetActive(false); //this makes it return to the pool

    }
    public void ChangeText(string text)
    {
        
        TextRef.text = text; //content fitter deals with the box fitting the text

    }


    private void Update()
    {
        if (targetPosition != null)
        {
            transform.position = Vector3.Lerp(transform.position, (Vector3)targetPosition, slideSpeed*Time.deltaTime);
            if (Vector3.Distance(transform.position, (Vector3)targetPosition) < 1f)
            {
                targetPosition = null; //stop moving
            }
        }
    }

}
