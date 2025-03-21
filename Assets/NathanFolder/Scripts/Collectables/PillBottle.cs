using UnityEngine;
using UnityEngine.Events;

public class PillBottle : MonoBehaviour, IInteractable
{
    public UnityEvent TakePills;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void InteractedWith()
    {
        TakePills.Invoke();
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
