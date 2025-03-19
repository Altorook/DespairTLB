using UnityEngine;

public class MoveObject : MonoBehaviour, IInteractable
{
    [SerializeField] Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void InteractedWith()
    {
        if(rb != null)
        {
            rb.AddForce(new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
