using UnityEngine;
using UnityEngine.Events;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenuPanel;
    public UnityEvent DisablePlayerControls;
    public UnityEvent EnablePlayerControls;

    void Start()
    {
        PauseMenuPanel.SetActive(false);
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenuPanel.SetActive(true);
        DisablePlayerControls.Invoke();
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenuPanel.SetActive(false);
        EnablePlayerControls.Invoke();
    }
     public void QuitGame()
    {
        Application.Quit();
    }
}
