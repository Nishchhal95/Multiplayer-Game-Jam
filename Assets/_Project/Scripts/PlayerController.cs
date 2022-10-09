using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    // WASD INPUT
    public float speed = 10.0f; 
    private float horizontalInput;
    private float verticalInput;
    public CharacterController playerController;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private MainGameManager mainGameManager;

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        mainGameManager = FindObjectOfType<MainGameManager>();
    }

    private void Start()
    {
        if (!_photonView.IsMine)
        {
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_photonView.IsMine || cameraTransform == null)
        {
            return;
        }

        PlayerRotation();
        MovementInput();
    }

    private void PlayerRotation()
    {
        Vector3 forwardOrientation = transform.position -
                            new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);
        transform.forward = forwardOrientation.normalized;
    }

    private void MovementInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
       
        verticalInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        
        playerController.Move(move * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_photonView.IsMine)
        {
            return;
        }
        if (other.CompareTag("Bottle"))
        {
            string bottleName = other.gameObject.name;
            _photonView.RPC("DisableBottle", RpcTarget.AllBuffered, bottleName);
        }
    }

    [PunRPC]
    private void DisableBottle(string bottleName)
    {
        mainGameManager.DisableBottle(bottleName);
    }

    public void SetCamera(Transform playerCameraTransform)
    {
        cameraTransform = playerCameraTransform;
    }
}
