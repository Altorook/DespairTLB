using UnityEngine;
using UnityEngine.Events;

public class FlashlightBattery : MonoBehaviour , IInteractable
{
    public UnityEvent CollectBattery;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void InteractedWith()
    {
        CollectBattery.Invoke();
        Destroy(this.gameObject); 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
