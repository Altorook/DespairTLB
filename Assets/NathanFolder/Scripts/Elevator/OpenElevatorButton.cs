using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OpenElevatorButton : MonoBehaviour, IInteractable
{
    [SerializeField] Transform LeftDoor;
    [SerializeField] Transform RightDoor;
    bool isInteractedWith;
    bool opening;
    bool closing;
    float startXLeft;
    float endXLeft;
   [SerializeField] float openDistance;
    [SerializeField] float openRate;
    [SerializeField] float openDuration;
    float lerpTime;
    float startXRight;
    float endXRight;
    bool isOpen;
    bool isClose = true;
    bool hasBeenOpened = false;
    public UnityEvent EnableFloor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       startXLeft = LeftDoor.localPosition.y;
        startXRight = RightDoor.localPosition.y;
        endXLeft = LeftDoor.localPosition.y + openDistance;
        endXRight = RightDoor.localPosition.y - openDistance;
    }
    public void InteractedWith()
    {
        if (!isOpen && !opening && !closing)
        {
            StartCoroutine(Open());
            if(hasBeenOpened == false)
            {
                EnableFloor.Invoke();
                hasBeenOpened = true;
            }
        }
    }
    IEnumerator Open()
    {
        opening = true;
        closing = false;
        lerpTime = 0;
        yield return new WaitForSeconds(openDuration);
        closing = false;
        opening = false;
        lerpTime = 0;
        isOpen = true;
        isClose = false ;
    }
    IEnumerator Close()
    {
        opening = false;
        closing = true;
        lerpTime = 0;
        yield return new WaitForSeconds(openDuration);
        closing = false;
        opening = false;
        lerpTime = 0;
        isOpen = false;
        isClose = true;
    }
    void ClosingProcess()
    {
     //   Debug.Log("closing");
        lerpTime += Time.deltaTime * openRate;
        LeftDoor.localPosition =new Vector3(LeftDoor.localPosition.x, Mathf.Lerp(endXLeft, startXLeft, lerpTime),LeftDoor.localPosition.z );
        RightDoor.localPosition = new Vector3(RightDoor.localPosition.x, Mathf.Lerp(endXRight, startXRight, lerpTime), RightDoor.localPosition.z);
    }
    void OpeningProcess()
    {
       // Debug.Log("opening");
        lerpTime += Time.deltaTime * openRate;
        LeftDoor.localPosition = new Vector3(LeftDoor.localPosition.x, Mathf.Lerp(startXLeft, endXLeft, lerpTime), LeftDoor.localPosition.z);
        RightDoor.localPosition = new Vector3(RightDoor.localPosition.x, Mathf.Lerp(startXRight, endXRight, lerpTime), RightDoor.localPosition.z);
    }
    public void EnableOpen()
    {
        if( !isOpen && !opening && !closing)StartCoroutine(Open());
    }
    public void EnableClose()
    {
        if(!isClose && !opening && !closing) StartCoroutine(Close());
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (opening)
        {
            OpeningProcess();
        }
        if (closing)
        {
            ClosingProcess();
        }
    }
}
