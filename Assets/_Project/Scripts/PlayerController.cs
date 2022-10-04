using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private float groundedOffset = .14f;
    [SerializeField] private float groundedRadius = .28f;
    [SerializeField] private LayerMask groundLayer;
    private bool _grounded;

    [Header("Gravity")]
    [SerializeField] private float gravity = -15f;
    [SerializeField] private float jumpHeight = 5f;
    private float _verticalVelocity;
    private bool _jump;

    [Header("Movement")]
    [SerializeField] private bool rawInput;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    private float _playerSpeed = 10f;
    private Vector3 _inputVector;
    private Vector3 _movement;
    private bool _isSprinting;

    [SerializeField] private CharacterController characterController;
    
    [SerializeField] private PhotonView photonView;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        HandleInput();
        GroundCheck();
        Jump();
        ApplyGravity();
        Move();
    }

    private void HandleInput()
    {
        _inputVector.x = rawInput ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal");
        _inputVector.z = rawInput ? Input.GetAxisRaw("Vertical") : Input.GetAxis("Vertical");

        _isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jump = true;
        }
    }

    private void GroundCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, 
            transform.position.y - groundedOffset, transform.position.z);
        _grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayer, QueryTriggerInteraction.Ignore);
    }
    
    private void Jump()
    {
        if (_jump && _grounded)
        {
            _jump = false;
            // the square root of H * -2 * G = how much velocity needed to reach desired height
            _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        _verticalVelocity += gravity * Time.deltaTime;
        if (_grounded && _verticalVelocity < 0.0f)
        {
            _verticalVelocity = -2f;
        }
    }

    private void Move()
    {
        _playerSpeed = _isSprinting ? sprintSpeed : walkSpeed;
        _movement.y = _verticalVelocity;
        characterController.Move(_movement * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (_grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;
			
        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(new Vector3(transform.position.x, 
            transform.position.y - groundedOffset, transform.position.z), groundedRadius);
    }
}
