using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private PlayerControls playerControls;
    private Animator animator;
    private InventoryManager inventory;
    private MainMenuSettings menu;


    public GameObject MG; // magnifying glass object

    public GameObject introCam; // allows access to inIntro boolean 

    [HideInInspector]
    public GameObject manager;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    private Vector3 movement;
    private Vector2 movementInput;
    private bool isMoving;
    private float velocity;
    public GameObject dressUpManager;


    [SerializeField]
    private float playerSpeed = 2.5f;

    [SerializeField]
    private float acceleration = 2.0f;

    [SerializeField]
    private float deceleration = 3.0f;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private GameObject dialogueZone;

    bool inspectingItem;

    public bool inLD = false;
    public GameObject conclusionManager;
    public GameObject interrogationManager;
    public GameObject tutorialManager;
    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inventory = FindObjectOfType<InventoryManager>();
        menu = FindObjectOfType<MainMenuSettings>();

        // holds a map of inputs for the player
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
        Cursor.visible = false;
    }

    void Update()
    {
        if(cameraTransform == null && !introCam.GetComponent<IntroCutscene>().inIntro)
        {
            cameraTransform = Camera.main.transform;
        }

        isMoving = movement != Vector3.zero;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (isMoving && !MG.GetComponent<MagnifyingGlass>().usingMagnifyingGlass)
        {
            gameObject.transform.forward = movement;
        }

        if(MG.GetComponent<MagnifyingGlass>().evidenceItem)
        {
            if(MG.GetComponent<MagnifyingGlass>().evidenceItem.GetComponent<EvidenceItem>().inspectingItem)
            {
                inspectingItem = true;
            }
            else
            {
                inspectingItem = false;
            }
        }

        // stops player movement & disables camera whilst UI open
        if (inventory.UIVisibility.inventoryOpen || inventory.UIVisibility.pinboardOpen 
            || dialogueZone.activeInHierarchy || manager.GetComponent<SceneTransition>().interrogationActive 
            || inventory.UIVisibility.jotterOpen || dressUpManager.GetComponent<DressUp>().inDressUp || manager.GetComponent<IntroCutscene>().inIntro
            || menu.menuOpen || inspectingItem || inventory.UIVisibility.notebookOpen || inLD || conclusionManager.GetComponent<Conclusion>().inEnding || 
            interrogationManager.GetComponent<Interrogation>().inInterrogation || tutorialManager.GetComponent<Tutorials>().mGlassTutorial5 || tutorialManager.GetComponent<Tutorials>().lockMovement
             || tutorialManager.GetComponent<Tutorials>().lockMovementMainGame)
        {
            animator.Play("Idle");
            animator.SetFloat("Velocity", 0);

            GameObject.FindGameObjectWithTag("Camera").GetComponent<Cinemachine.CinemachineInputProvider>().enabled = false;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Camera").GetComponent<Cinemachine.CinemachineInputProvider>().enabled = true;
            HandleMovement();
        }

        HandleAnimation();
    }

    // Called when we want the player to be able to move (character moves forward in direction camera is facing)
    void HandleMovement()
    {
        // gets movement input from the input map (keys assigned in Move)
        movementInput = playerControls.PlayerInputMap.Move.ReadValue<Vector2>();
        movement = new Vector3(movementInput.x, 0, movementInput.y);
        movement = cameraTransform.forward * movement.z + cameraTransform.transform.right * movement.x;
        movement.y = 0;

        // adds movement to the players controller transform
        controller.Move(playerSpeed * Time.deltaTime * movement);

        // allows the player to fall from height
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(MG.GetComponent<MagnifyingGlass>().usingMagnifyingGlass)
        {
            HandleFirstPersonMovement();
            playerSpeed = 1.0f;
        }
        else
        {
            playerSpeed = 2.5f;
        }
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

        if(!MG.GetComponent<MagnifyingGlass>().usingMagnifyingGlass)
        {
            animator.SetFloat("Velocity", velocity);
        }
    }

    void HandleFirstPersonMovement()
    {
        animator.Play("Idle");
        animator.SetFloat("Velocity", 0);

        Quaternion rotation = cameraTransform.rotation;
        rotation.x = 0;
        rotation.z = 0;
        transform.SetLocalPositionAndRotation(movement, rotation);
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

   
}
