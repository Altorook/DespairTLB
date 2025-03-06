using UnityEngine;

public class InteractableDoor : MonoBehaviour, IInteractable
{
    bool isOpen = false;
    bool isOpening = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void InteractedWith()
    {
        if(isOpen == false)
        {
            float xRot = this.transform.rotation.x;
            this.transform.Rotate(new Vector3(0,0,135));
            isOpen = true;
            isOpening = true;
            Debug.Log("Open?");
        }
    }
    public void OpenDoor()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
