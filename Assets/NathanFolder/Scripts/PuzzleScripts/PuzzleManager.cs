using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    bool isRotationPuzzleCompleted;
    bool isPasswordPuzzleCompleted;
    int amountOfRotationPuzzlesCompleted;
    public UnityEvent RotationPuzzleDoor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RotPuzzleWasSolved()
    {
        amountOfRotationPuzzlesCompleted++;
        if(amountOfRotationPuzzlesCompleted >= 3)
        {
            isRotationPuzzleCompleted = true;
            RotationPuzzleDoor.Invoke();
        }
    }
}
