using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorLevelButton : MonoBehaviour , IInteractable
{
    [SerializeField] GameObject LevelOfButton;
    [SerializeField] GameObject LevelThisElevator;
    [SerializeField] bool isOnFloorOfButton;
    [SerializeField] Transform playerTransform;
    public UnityEvent OpenDesiredFloor;
    public UnityEvent CloseThisElevator;
    Vector3 parentRelativePos;
    Quaternion parentRelativeRot;
    [SerializeField] bool canAccessFloor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void EnableFloor()
    {
        canAccessFloor = true;
    }
    public void InteractedWith()
    {
        if(!isOnFloorOfButton && canAccessFloor)
        {
            StartCoroutine(WaitForElevator());
            
            CloseThisElevator.Invoke();
        }
    }
    IEnumerator WaitForElevator()
    {
        //play elevator noise
        yield return new WaitForSeconds(7);
        Debug.Log("Attempt Teleport");
        // playerTransform.position = LevelOfButton.transform.position;
        //playerTransform.SetParent(LevelOfButton.transform);
        playerTransform.SetParent(LevelThisElevator.transform);
        yield return new WaitForSeconds(1);
        parentRelativePos = playerTransform.localPosition;
        parentRelativeRot = playerTransform.localRotation;
        
        yield return new WaitForSeconds(1);
        playerTransform.SetParent(LevelOfButton.transform);
        yield return new WaitForSeconds(1);
        playerTransform.gameObject.GetComponent<CharacterController>().enabled = false;
        playerTransform.SetLocalPositionAndRotation(parentRelativePos, parentRelativeRot);
        playerTransform.gameObject.GetComponent<CharacterController>().enabled = true;
        OpenDesiredFloor.Invoke();
        yield return new WaitForSeconds(.2f);
        playerTransform.SetParent(null);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
