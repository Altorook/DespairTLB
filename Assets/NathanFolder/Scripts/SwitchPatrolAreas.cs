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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SwitchArea.Invoke();
        }
    }
}
