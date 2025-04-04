using UnityEngine;

public class PuzLightManager : MonoBehaviour
{
    [SerializeField] Material redMat;
    [SerializeField] Material greenMat;
    [SerializeField] MeshRenderer meshyBoy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void EnabledMaterial()
    {
        meshyBoy.materials[1] = greenMat;
    }
    public void DisabledMaterial()
    {
        meshyBoy.materials[1] = redMat;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
