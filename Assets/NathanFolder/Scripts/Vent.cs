using UnityEngine;

public class Vent : MonoBehaviour
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
    private void OnCollisionEnter(Collision collision)
    {
      if(collision.gameObject.tag == "Player")
        {
            status.isVented = true;
            sanity.isVented = true;
        }  
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            status.isVented = false;
            sanity.isVented = false;
        }
    }
}
