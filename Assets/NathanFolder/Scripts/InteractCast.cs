using UnityEngine;

public class InteractCast : MonoBehaviour
{
 /*   [SerializeField] Transform cameraTransform;*/
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    LayerMask layerMaskWPlayer;
    [SerializeField] float castDistance;
    [SerializeField] GameObject interactText;
    bool uiOpen;
    [SerializeField] GameObject CrossHair;
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
    public void OpenedUI() {  uiOpen = true; }
    public void ClosedUI() {  uiOpen = false; }
    private void FixedUpdate()
    {
        interactText.SetActive(false);
        RaycastHit hit;
        if (!uiOpen)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            
           


                if (Physics.Raycast(ray.origin, ray.direction, out hit, castDistance, ~layerMask))
                {
                    if (hit.transform.gameObject.GetComponent<IInteractable>() != null && !uiOpen)
                    {
                   // if (!Physics.Raycast(ray.origin, ray.direction, out hit, Vector3.Distance(hit.transform.position, this.transform.position), ~layerMaskWPlayer.value))
                  //  {
                        interactText.SetActive(true); CrossHair.SetActive(true);
                  //  }                    
                    }
                    else
                    {
                        interactText.SetActive(false); CrossHair.SetActive(false);
                    }
                }
            
        }    
    }
    void CastTheRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        if(Physics.Raycast(ray.origin, ray.direction,out hit , castDistance , ~layerMask))
        {
           // if (!Physics.Raycast(ray.origin, ray.direction, Vector3.Distance(hit.transform.position, this.transform.position), ~layerMaskWPlayer.value))
           // {
              //  Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject.GetComponent<IInteractable>() != null) hit.transform.gameObject.GetComponent<IInteractable>().InteractedWith();
          //  }
        }

    }
}
