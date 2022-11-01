using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private PlayerControls playerControls;
    private Animator animator;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    private Vector3 movement;
    private Vector2 movementInput;
    private bool isMoving;
    private float velocity;

    [SerializeField]
    public float playerSpeed = 4.0f;

    [SerializeField]
    private float acceleration = 2.0f;

    [SerializeField]
    private float deceleration = 3.0f;

    [SerializeField]
    public float jumpHeight = 1.0f;

    [SerializeField]
    private Transform cameraTransform;

    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;

        // holds a map of inputs for the player
        playerControls = new PlayerControls();
    }

    void Update()
    {
        isMoving = movement != Vector3.zero;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (isMoving)
        {
            gameObject.transform.forward = movement;
        }

        // Changes the height position of the player..
        if (playerControls.PlayerInputMap.Jump.IsPressed() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        HandleMovement();
        HandleAnimation();
    }

    void HandleMovement()
    {
        movementInput = playerControls.PlayerInputMap.Move.ReadValue<Vector2>();
        movement = new Vector3(movementInput.x, 0, movementInput.y);
        movement = cameraTransform.forward * movement.z + cameraTransform.transform.right * movement.x;
        movement.y = 0;

        controller.Move(movement * Time.deltaTime * playerSpeed);
    }

    void HandleAnimation()
    {
        // blends between walk/run animations as velocity increases / decreases
        if(isMoving && velocity < 1.0f)
        {
            velocity += Time.deltaTime * acceleration;
            velocity = Mathf.Clamp(velocity, 0, 1);
        }
        if(!isMoving && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * deceleration;
            velocity = Mathf.Clamp(velocity, 0, 1);
        }

        animator.SetFloat("Velocity", velocity);
    }

    // enables / disables input system
    private void OnEnable()
    {
        playerControls.PlayerInputMap.Enable();
    }

    private void OnDisable()
    {
        playerControls.PlayerInputMap.Disable();
    }

        // hide mouse cursor when player is focus
    private void OnApplicationFocus(bool focus)
    {
        if(focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}