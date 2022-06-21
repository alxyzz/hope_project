using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CharacterController_New : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;


    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        float curSpeedX = canMove ?  walkingSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? walkingSpeed * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            moveDirection.y = movementDirectionY;
        moveDirection = -moveDirection;

       
        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
