using UnityEngine;
using UnityEngine.Events;

public class SwitchPatrolAreas : MonoBehaviour
{
    public UnityEvent SwitchArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer == 11)
        {
            SwitchArea.Invoke();
        }
    }
}
