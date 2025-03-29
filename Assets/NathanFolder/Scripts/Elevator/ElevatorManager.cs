using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] ElevatorLevelButton[] AllElevatorButtons;
    void Start()
    {
        
    }
    public void DisableElevatorButtons()
    {
        for(int i = 0; i < AllElevatorButtons.Length; i++)
        {
            AllElevatorButtons[i].CannotInteract();
        }
    }
    public void EnableElevatorButtons()
    {
        for (int i = 0; i < AllElevatorButtons.Length; i++)
        {
            AllElevatorButtons[i].AllowInteract();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
