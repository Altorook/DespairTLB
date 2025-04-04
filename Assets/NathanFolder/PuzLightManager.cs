using UnityEngine;

public class PuzLightManager : MonoBehaviour
{
    [SerializeField] GameObject greenLightObject;
    [SerializeField] GameObject redLightObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }
    public void EnabledMaterial()
    {
     greenLightObject.SetActive(true); redLightObject.SetActive(false);
    }
    public void DisabledMaterial()
    {
        greenLightObject.SetActive(false); redLightObject.SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
     
    }
}
