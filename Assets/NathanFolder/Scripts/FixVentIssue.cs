using UnityEngine;

public class FixVentIssue : MonoBehaviour
{
    [SerializeField] SOVentStatus status;
    [SerializeField] SOSanity sanity;
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
        if (collision.gameObject.layer == 11)
        {
            status.isVented = false;
            sanity.isVented = false;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 11)
        {
            status.isVented = false;
            sanity.isVented = false;
        }
    }
}
