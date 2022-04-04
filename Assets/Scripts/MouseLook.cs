using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float mouseSensitivity = 200f;
    public void SetMouseSensitivity(float value)
    {
        mouseSensitivity = value;
    }
    
    private float xRotation = 0f;
    
    private Transform playerBody;

    void Start()
    {
        playerBody = transform.parent;
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    
    
    // === GETTERS ===
    public float GetMouseSensitivity()
    {
        return mouseSensitivity;
    }
}
