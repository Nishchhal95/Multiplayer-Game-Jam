using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private float horizontalInput;
    private float verticalInput;

    public CharacterController playerController;

    

    // Update is called once per frame
    void Update()
    {
        
        MovementInput();
        
    }

    private void MovementInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
       
        verticalInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;


        playerController.Move(move * speed *Time.deltaTime);
    }
}
