using System.Collections;
using UnityEngine;

public class TiltPainting : MonoBehaviour, IInteractable
{
    bool isTilted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InteractedWith()
    {
        if(isTilted == false)
        {
            StartCoroutine(HoldAngle());
        }
    }
    IEnumerator HoldAngle()
    {
        isTilted = true;
        this.transform.parent.transform.rotation = Quaternion.Euler(this.transform.parent.transform.rotation.x + 30, this.transform.parent.transform.rotation.y, this.transform.parent.transform.rotation.z);
        yield return new WaitForSeconds(5);
        isTilted = false;
        this.transform.parent.transform.rotation = Quaternion.Euler(this.transform.parent.transform.rotation.x, this.transform.parent.transform.rotation.y, this.transform.parent.transform.rotation.z);

    }
}
