using UnityEngine;
using UnityEngine.Events;

public class RotationPuzzleBox : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject PuzzleCanvas;
    public UnityEvent UIOpen;
    bool isOpen = false;
    void Start()
    {
        
    }
    public void InteractedWith()
    {
        if(isOpen == false)
        {
            PuzzleCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UIOpen.Invoke();
            isOpen = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            PuzzleCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UIOpen.Invoke();
            isOpen = false;
        }
    }
}
