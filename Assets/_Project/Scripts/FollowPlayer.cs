using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private float mouseX;
    private float mouseY;

    public float lookSpeed = 10.0f;

    float xRotation = 0f;

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // keep the cursor on screen
    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();  
    }

    public void MouseLook()
    {
        mouseX = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;

        xRotation -= mouseY; // up and down angle
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // limit up down movement

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        playerTransform.Rotate(Vector3.up * mouseX); //left and right watch
    }
}
