using UnityEngine;

public class HideAllDesktops : MonoBehaviour
{

    [SerializeField] GameObject[] allDesktops;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void HideDesktops()
    {
        for(int i = 0; i<allDesktops.Length; i++)
        {
            allDesktops[i].SetActive(false);
        }
    }
    public void ShowDesktops()
    {
        for (int i = 0; i < allDesktops.Length; i++)
        {
            allDesktops[i].SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
