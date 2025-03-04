using UnityEngine;

public class InteractCast : MonoBehaviour
{
 /*   [SerializeField] Transform cameraTransform;*/
    [SerializeField]
    LayerMask layerMask;
    [SerializeField] float castDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            CastTheRay();
        }
    }
    void CastTheRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        if(Physics.Raycast(ray.origin, ray.direction,out hit , castDistance , layerMask))
        {
            Debug.Log(hit.transform.gameObject.name);
            if(hit.transform.gameObject.GetComponent<IInteractable>() != null) hit.transform.gameObject.GetComponent<IInteractable>().InteractedWith();

        }

    }
}
