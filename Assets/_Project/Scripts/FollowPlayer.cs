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
    public Transform lookTarget;
    public Vector2 look;

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

        

        lookTarget.Rotate(Vector3.up * mouseX); //left and right watch


        //VERTICAL ROTATION

        lookTarget.transform.rotation *= Quaternion.AngleAxis(look.y * lookSpeed, Vector3.right);

        var angles = lookTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = lookTarget.transform.localEulerAngles.x;

        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        lookTarget.transform.localEulerAngles = angles;

        //setting player based on look target
        playerTransform.rotation = Quaternion.Euler(0, lookTarget.transform.eulerAngles.y, 0);
        //reset look target, dont know why
        lookTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

    }
}
