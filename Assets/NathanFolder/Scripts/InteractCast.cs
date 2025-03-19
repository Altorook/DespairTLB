using UnityEngine;

public class InteractCast : MonoBehaviour
{
 /*   [SerializeField] Transform cameraTransform;*/
    [SerializeField]
    LayerMask layerMask;
    [SerializeField] float castDistance;
    [SerializeField] GameObject interactText;
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
    private void FixedUpdate()
    {
        interactText.SetActive(false);
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray.origin, ray.direction, out hit, castDistance, layerMask))
        {
            if (hit.transform.gameObject.GetComponent<IInteractable>() != null) interactText.SetActive(true);else interactText.SetActive(false);

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
