using UnityEngine;

public class SafeAreaVolume : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] SOSanity soSanity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            soSanity.isInSafeArea = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            soSanity.isInSafeArea = false;
        }
    }
}
