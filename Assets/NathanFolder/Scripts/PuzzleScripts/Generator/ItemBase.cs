using UnityEngine;
using UnityEngine.Events;

public class ItemBase : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject[] ReactorCores;
    [SerializeField] GameObject[] lightsForCores;
    int numOfPlacedCores = 0;
    public UnityEvent OpenLastDoor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void InteractedWith()
    {
        ReactorCores[numOfPlacedCores].SetActive(true);
        //lightsForCores[numOfPlacedCores].colorify;
        numOfPlacedCores++;
        if(numOfPlacedCores >= 3)
        {
            OpenLastDoor.Invoke();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
