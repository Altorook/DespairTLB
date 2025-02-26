using UnityEngine;

public class PlayerLineOfSight : MonoBehaviour
{
    [SerializeField] 
    Transform ViewOrigin;
    [SerializeField]
    LayerMask layerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay(Collider other)
    {
       // Debug.Log("Looking At " + other.gameObject.name);
        RaycastHit hit;
        Debug.Log("CannotMove");
        Debug.DrawRay(ViewOrigin.position, other.transform.position - ViewOrigin.position);
        if (Physics.Raycast(ViewOrigin.position, other.transform.position - ViewOrigin.position, out hit, Vector3.Distance(ViewOrigin.position, other.transform.position), ~layerMask.value))
        {
            
         //   Debug.Log(hit.transform.gameObject.name);
            Debug.Log("CanMove");
        }
    }
}
