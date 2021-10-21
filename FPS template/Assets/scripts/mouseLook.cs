using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public float mouseSensitivity=100f;
    public Transform playerBody;
    float xRotation=0f;
    void Start()
    {
      Cursor.lockState= CursorLockMode.Locked;  //to hide the cursor
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX= Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;
        float mouseY= Input.GetAxis("Mouse Y")*mouseSensitivity*Time.deltaTime;
        xRotation-=mouseY;
        xRotation= Mathf.Clamp(xRotation,-90f,67f); //to limit the vertical rotation

        transform.localRotation= Quaternion.Euler(xRotation,0f,0f); //rotating the camera for vertical look
        playerBody.Rotate(Vector3.up*mouseX);  //rotating whole body (along with camera) for horizontal look
        
    }
}
