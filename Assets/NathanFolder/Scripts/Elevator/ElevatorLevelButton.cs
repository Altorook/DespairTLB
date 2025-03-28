using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorLevelButton : MonoBehaviour , IInteractable
{
    [SerializeField] GameObject LevelOfButton;
    [SerializeField] bool isOnFloorOfButton;
    [SerializeField] Transform playerTransform;
    public UnityEvent OpenDesiredFloor;
    public UnityEvent CloseThisElevator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void InteractedWith()
    {
        if(!isOnFloorOfButton)
        {
            StartCoroutine(WaitForElevator());
            OpenDesiredFloor.Invoke();
            CloseThisElevator.Invoke();
        }
    }
    IEnumerator WaitForElevator()
    {
        //play elevator noise
        yield return new WaitForSeconds(4);
        playerTransform.position = LevelOfButton.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
