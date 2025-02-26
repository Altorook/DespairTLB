using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class PlayerLineOfSight : MonoBehaviour
{
    [SerializeField] 
    Transform ViewOrigin;
    [SerializeField]
    LayerMask layerMask;
    public UnityEvent EnemyLookedAt;
    public UnityEvent EnemyNotLookedAt;
    private bool isEnemyLookedAt = false;
    private bool didLookStateChange = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      //  Debug.DrawRay();
    }
    void FixedUpdate()
    {
        if(isEnemyLookedAt != didLookStateChange)
        {
            if (isEnemyLookedAt)
            {
                EnemyLookedAt.Invoke();
            }
            else
            {
                EnemyNotLookedAt.Invoke();
            }
            didLookStateChange = isEnemyLookedAt;
        }
    }
    public void OnTriggerStay(Collider other)
    {
       // Debug.Log("Looking At " + other.gameObject.name);
        RaycastHit hit;
        Debug.DrawRay(ViewOrigin.position, other.transform.position - ViewOrigin.position);
        if (Physics.Raycast(ViewOrigin.position, other.transform.position - ViewOrigin.position, out hit, Vector3.Distance(ViewOrigin.position, other.transform.position), ~layerMask.value))
        {
            isEnemyLookedAt = false;
         //   Debug.Log(hit.transform.gameObject.name);
            Debug.Log("CanMove");
        }
        else
        {
            isEnemyLookedAt = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        isEnemyLookedAt = false;
    }
}
