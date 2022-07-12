using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInputManager : MonoBehaviour
{
    /// <summary>
    /// Basically, this is so we can click on UI Canvas objects
    /// </summary>
    public EventSystem eventSys;
    public GraphicRaycaster gRaycaster;

    void Update()
    {
        //if (DataStorage.GameManagerComponent.InputComponent.clicked) // upon click
        //{
        //    DataStorage.raycastResultList = new(); // clears raycast list everytime
        //    // creates new pointer event, sets position to mouse, raycasts only into UI Canvas
        //    DataStorage.pointerEventData = new PointerEventData(eventSys);
        //    DataStorage.pointerEventData.position = Input.mousePosition;
        //    gRaycaster.Raycast(DataStorage.pointerEventData, DataStorage.raycastResultList);
        //    foreach (RaycastResult result in DataStorage.raycastResultList)
        //    {
        //        if (result.gameObject.GetComponent<InventoryUIObject>()) // we can filter the list to access objects that we want
        //        {
        //            Debug.LogWarning("Graphic Raycast hit " + result.gameObject.name);
        //            result.gameObject.GetComponent<InventoryUIObject>().itemAction.Invoke(); // invokes unity event
        //        }
        //    }
        //}
    }
}
