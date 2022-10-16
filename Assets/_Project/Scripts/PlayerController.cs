using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private Vector3 moveDirection;
    private Animator playerAnim;

    private static int potPlayerCount = 0;

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

        playerAnim = GetComponentInChildren<Animator>();
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

        moveDirection = new Vector3(0, 0, verticalInput);

        if(moveDirection== Vector3.zero)
        {
            Idle();

        }
        else if(moveDirection != Vector3.zero)
        {
            Run();
        }
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

        if (other.CompareTag("Pot"))
        {
            _photonView.RPC("PotPlayer", RpcTarget.AllBuffered);
        }
        
        if (other.CompareTag("Portal"))
        {
            mainGameManager.LoadFinalScene();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_photonView.IsMine)
        {
            return;
        }
        if (other.CompareTag("Pot"))
        {
            _photonView.RPC("PotPlayerReduce", RpcTarget.AllBuffered);
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

    private void Idle()
    {
        playerAnim.SetFloat("vertical", 0);
    }

    private void Run()
    {
        playerAnim.SetFloat("vertical", 0.9f);
    }
    
    [PunRPC]
    private void PotPlayer()
    {
        potPlayerCount++;
        if (potPlayerCount >= 2)
        {
            _photonView.RPC("ShowPortalVFX", RpcTarget.AllBuffered);
        }
    }
    
    [PunRPC]
    private void PotPlayerReduce()
    {
        potPlayerCount--;
    }
    
    [PunRPC]
    private void ShowPortalVFX()
    {
        mainGameManager.ShowPortal();
    }
}
