using System.Collections;
using UnityEngine;

public class InteractableDoor : MonoBehaviour, IInteractable
{
    bool isOpen = false;
    bool isOpening = false;
    float amountOpen = 0;
    [SerializeField] float amountToOpen;
    [SerializeField]
    float openSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void InteractedWith()
    {
        if(isOpen == false)
        {
           
           
            isOpen = true;
            isOpening = true;
            Debug.Log("Open?");
            OpenDoor();
        }
    }
    public void OpenDoor()
    {
        
        if(amountOpen < amountToOpen)
        {
            amountOpen += openSpeed;
            this.transform.Rotate(new Vector3(0, 0, openSpeed));
            StartCoroutine(SmoothOpen());
        }
    }
    IEnumerator SmoothOpen()
    {
        yield return new WaitForFixedUpdate();
        OpenDoor();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       
    }
}
