using UnityEngine;

public class KeypadInteraction : MonoBehaviour, IInteractable
{
    [Header("Reference")]
    public GameObject keypadCanvas;
    public Camera mainCamera;
    public Camera keypadCamera;
    public GameObject player;
    public string movementScriptName = "PlayerMovement";
    private MonoBehaviour movementScript;
    private bool isUsingKeypad = false;

    private void Start()
    {
        movementScript = player.GetComponent(movementScriptName) as MonoBehaviour;

        if (movementScript == null)
        {
            Debug.LogError($"Movement script '{movementScriptName}' not found on {player.name}");
        }
    }

    public void InteractedWith()
    {
        if (!isUsingKeypad)
        {
            isUsingKeypad = true;
            mainCamera.gameObject.SetActive(false);
            keypadCamera.gameObject.SetActive(true);
            keypadCanvas.SetActive(true);

            if (movementScript != null)
            {
                player.SendMessage("ToggleMenuState");
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isUsingKeypad)
        {
            ExitKeypad();
        }
    }

    private void ExitKeypad()
    {
        isUsingKeypad = false;
        mainCamera.gameObject.SetActive(true);
        keypadCamera.gameObject.SetActive(false);

        if (movementScript != null)
        {
            player.SendMessage("ToggleMenuState");
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        keypadCanvas.SetActive(false);
    }
}
