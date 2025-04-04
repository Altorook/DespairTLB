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
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        PauseMenuPanel.SetActive(true);
        DisablePlayerControls.Invoke();
    }

    public void ResumeGame()
    {
        PauseMenuPanel.SetActive(false);
        EnablePlayerControls.Invoke();
    }
}
