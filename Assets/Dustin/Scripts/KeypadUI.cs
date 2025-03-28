using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyPadUI : MonoBehaviour
{
    [Header("References")]
    public KeypadManager codeManager;
    public GameObject keypadWindow;

    [Header("UI Elements")]
    public TMP_Text inputDisplayText;
    public TMP_Text feedbackText;
    public Button[] numberButtons; // Make sure to assign 0-9 buttons in order!
    public Button backspaceButton;
    public Button clearButton;
    public Button submitButton;
    public Button closeButton;

    private string currentInput = "";

    private void Start()
    {
        keypadWindow.SetActive(false);
        feedbackText.text = "";
        inputDisplayText.text = "";

        // Hook up number buttons
        for (int i = 0; i < numberButtons.Length; i++)
        {
            int digit = i; // capture loop variable
            numberButtons[i].onClick.AddListener(()  => AddDigit(digit.ToString()));
        }

        backspaceButton.onClick.AddListener(Backspace);
        clearButton.onClick.AddListener(ClearInput);
        submitButton.onClick.AddListener(CheckCode);
        closeButton.onClick.AddListener(CloseWindow);
    }

    public void OpenKeypad()
    {
        keypadWindow.SetActive(true);
        ClearInput();
    }

    public void CloseWindow()
    {
        keypadWindow.SetActive(false);
        ClearInput();
        feedbackText.text = "";
    }

    private void AddDigit(string digit)
    {
        if (currentInput.Length < 4)
        {
            currentInput += digit;
            inputDisplayText.text = currentInput;
        }
    }

    private void Backspace()
    {
        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            inputDisplayText.text = currentInput;
        }
    }

    private void ClearInput()
    {
        currentInput = "";
        inputDisplayText.text = "";
    }

    private void CheckCode()
    {
        if (currentInput == codeManager.CorrectCode)
        {
            feedbackText.text = "Access Granted. Door Unlocked.";
            //AudioManager.PlaySound(5);
            //Debug.Log("[Keypad] Correct code entered.");
        }
        else
        {
            feedbackText.text = "Incorect Code!";
            //Debug.Log("[Keypad] Incorrect code.");
        }

        ClearInput();
    }
}