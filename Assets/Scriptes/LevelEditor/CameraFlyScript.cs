using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlyScript : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float lookSpeed = 2f;  
    public float maxLookAngle = 80f; 

    private float rotationX = 0f; 
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxLookAngle, maxLookAngle);

        
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection); 

        if (Input.GetMouseButton(1))
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(rotationX, transform.localEulerAngles.y + mouseX, 0);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }


    }
}

