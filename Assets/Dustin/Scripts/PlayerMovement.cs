using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 5.5f;
    [SerializeField] private float crouchSpeed = 2f;

    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 5f;
    [SerializeField] private float staminaRegen = 1f;
    [SerializeField] private float staminaDrainRate = 2f;
    private float currentStamina;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchHeight = 1.5f;
    private float originalHeight;
    private bool isCrouching = false;

    [Header("Look Settings")]
    [SerializeField] private float lookSensitivity = 0.3f;
    private float xRotation = 0f;

    private CharacterController controller;
    private PlayerInput playerInput;
    private InputAction moveAction, sprintAction, crouchAction, lookAction;
    private Camera playerCamera;

    private float cameraStandingHeight;
    private float cameraCrouchingHeight;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        
        // Ensures the camera is properly assigned
        playerCamera = Camera.main;
        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
            if (playerCamera == null)
            {
                Debug.LogError("No Camera found! Make sure there is a Camera is the Player GameObject.");
            }
        }

        moveAction = playerInput.actions["Move"];
        sprintAction = playerInput.actions["Sprint"];
        crouchAction = playerInput.actions["Crouch"];
        lookAction = playerInput.actions["Look"];

        originalHeight = controller.height;
        currentStamina = maxStamina;

        // Store camera heights based on starting position
        cameraStandingHeight = playerCamera.transform.localPosition.y;
        cameraCrouchingHeight = cameraStandingHeight - (originalHeight - crouchHeight);

        // Lock and hide cursor at start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleStamina();
        HandleLook();
        HandleCursor();
    }

    private void HandleMovement()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        float targetSpeed = walkSpeed;

        if (sprintAction.IsPressed() && currentStamina > 0 && !isCrouching)
        {
            targetSpeed = sprintSpeed;
            currentStamina -= staminaDrainRate * Time.deltaTime;
            Debug.Log("Player started sprinting");
        }
        else if (isCrouching)
        {
            targetSpeed = crouchSpeed;
        }
        else if (!sprintAction.IsPressed())
        {
            Debug.Log("Player stopped sprint");
        }

        Vector3 move = new Vector3(input.x, 0, input.y);
        if (move.sqrMagnitude > 1) move.Normalize();

        controller.Move((transform.right * move.x + transform.forward * move.z) * targetSpeed * Time.deltaTime);

        if (crouchAction.WasPressedThisFrame())
        {
            isCrouching = !isCrouching;
            controller.height = isCrouching ? crouchHeight : originalHeight;

            // Set absolute camera height
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                isCrouching ? cameraCrouchingHeight : cameraStandingHeight,
                playerCamera.transform.localPosition.z
                );

            Debug.Log(isCrouching ? "Player crouched." : "Player uncrouched");
        }
    }

    private void HandleStamina()
    {
        if (!sprintAction.IsPressed() && currentStamina < maxStamina)
        {
            currentStamina += staminaRegen * Time.deltaTime;
        }
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    private void HandleLook()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>() * lookSensitivity;

        // Rotate the camera up/down
        xRotation -= lookInput.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player left/right
        transform.Rotate(Vector3.up * lookInput.x);
    }

    private void HandleCursor()
    {
        // Gives mouse back when Esc Key is pressed. This will be refactored and redone when the UI is implemented. But this works for now.
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
        }
    }
}
