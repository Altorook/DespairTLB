using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LetterScript : MonoBehaviour,IInteractable
{
    [SerializeField] GameObject letterCanvas;
    [SerializeField] TMP_Text text;
    [SerializeField] SOPassword soPassword;
    public UnityEvent UIOpen;
    bool isOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void InteractedWith()
    {
        if (isOpen == false)
        {
            letterCanvas.SetActive(true);
            isOpen = true;
            text.SetText("Sincerely, Charles " + soPassword.middleName + " Harrington ");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UIOpen.Invoke();

        }
    }
    public void CloseCard()
    {
        letterCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UIOpen.Invoke();
        isOpen = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            CloseCard();
        }
    }
}
