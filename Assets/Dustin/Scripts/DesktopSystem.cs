using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DesktopSystem : MonoBehaviour
{
    [Header("References")]
    public PasswordManager passwordManager;
    public GameObject textFileWindow;
    public GameObject doorControlWindow;
    public TMP_InputField passwordInputField;
    public TMP_Text accessMessageText;
    public UnityEngine.UI.Button closeButtonTextFile;
    public UnityEngine.UI.Button closeButtonDoorControl;
    public UnityEngine.UI.Button submitPasswordButton;

    private void Start()
    {
        textFileWindow.SetActive(false);
        doorControlWindow.SetActive(false);
        closeButtonTextFile.onClick.AddListener(() => CloseWindow(textFileWindow));
        closeButtonDoorControl.onClick.AddListener(() => CloseWindow(doorControlWindow));
        submitPasswordButton.onClick.AddListener(CheckPassword);
    }

    public void OpenTextFile()
    {
        Debug.Log("Text File Button Pressed");
        textFileWindow.SetActive(true);
    }

    public void OpenDoorControl()
    {
        Debug.Log("Door Control Button Pressed");
        doorControlWindow.SetActive(true);
    }

    public void CloseWindow(GameObject window)
    {
        Debug.Log("Window Closed");
        window.SetActive(false);
    }

    private void CheckPassword()
    {
        if (passwordInputField.text == passwordManager.CorrectPassowrd)
        {
            accessMessageText.text = "Access Granted. Door Unlocked.";
            // Add door logic here later
        }
        else
        {
            accessMessageText.text = "Incorrect Password!";
        }
        passwordInputField.text = "";
    }
}