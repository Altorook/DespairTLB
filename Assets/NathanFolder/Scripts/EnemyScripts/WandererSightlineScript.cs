using UnityEngine;
using UnityEngine.Events;

public class WandererSightlineScript : MonoBehaviour
{
    [SerializeField]
    Transform ViewOrigin;
    [SerializeField]
    LayerMask layerMask;
    public UnityEvent PlayerLookedAt;
    public UnityEvent PlayerNotLookedAt;
    [SerializeField] WandererEnemy WandererEnemy;
    private bool isPlayerLookedAt = false;
    private bool didLookStateChange = false;
    [SerializeField] float timeToLoseEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (isPlayerLookedAt != didLookStateChange)
        {
            if (isPlayerLookedAt)
            {
                PlayerLookedAt.Invoke();
            }
            else
            {
                PlayerNotLookedAt.Invoke();
            }
            didLookStateChange = isPlayerLookedAt;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        // Debug.Log("Looking At " + other.gameObject.name);
        RaycastHit hit;
        Debug.DrawRay(ViewOrigin.position, other.transform.position - ViewOrigin.position);
        if (Physics.Raycast(ViewOrigin.position, other.transform.position - ViewOrigin.position, out hit, Vector3.Distance(ViewOrigin.position, other.transform.position), ~layerMask.value))
        {
            isPlayerLookedAt = false;
            //   Debug.Log(hit.transform.gameObject.name);
            Debug.Log("CanMove");
        }
        else
        {
            isPlayerLookedAt = true;
            WandererEnemy.timeOutOfSightBeforePatrol = timeToLoseEnemy;
            WandererEnemy.currentState = WandererEnemy.WandererState.Chase;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        isPlayerLookedAt = false;
    }
}
