using UnityEngine;

public class DesktopInteraction : MonoBehaviour
{
    [Header("References")]
    public GameObject desktopCanvas;
    public Camera mainCamera;
    public Camera computerCamera;
    public GameObject player;
    public string movementScriptName = "PlayerMovement";
    private MonoBehaviour movementScript;
    private bool isUsingComputer = false;

    private void Start()
    {
        movementScript = player.GetComponent(movementScriptName) as MonoBehaviour;

        if (movementScript == null)
        {
            Debug.LogError($"Movement script '{movementScriptName}' not found on {player.name}!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isUsingComputer)
        {
            UseComputer();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isUsingComputer)
        {
            ExitComputer();
        }
    }

    private void UseComputer()
    {
        isUsingComputer = true;
        mainCamera.gameObject.SetActive(false);
        computerCamera.gameObject.SetActive(true);
        desktopCanvas.SetActive(true);

        if (movementScript != null)
            movementScript.enabled = false; // Disables movement

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ExitComputer()
    {
        isUsingComputer = false;
        mainCamera.gameObject.SetActive(true);
        computerCamera.gameObject.SetActive(false);

        if (movementScript != null)
            movementScript.enabled = true; // Enables movement 

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}