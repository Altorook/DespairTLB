using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class CardScript : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject cardCanvas;
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
            cardCanvas.SetActive(true);
            isOpen = true;
            int age = soPassword.yearOfGame - int.Parse(soPassword.birthYear);
            if(age < 20)
            {
                text.SetText("Happy " + age + "th Birthday");
            }
            else if (age % 10 == 1)
            {
                text.SetText("Happy " + age + "st Birthday");
            }
            else if (age % 10 == 2)
            {
                text.SetText("Happy " + age + "nd Birthday");
            }
            else if (age % 10 == 3)
            {
                text.SetText("Happy " + age + "rd Birthday");
            }
            else
            {
                text.SetText("Happy " + age + "th Birthday");
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UIOpen.Invoke();

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            cardCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UIOpen.Invoke();
            isOpen = false;
        }
    }
}
