using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ItemBase : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject[] ReactorCores;
    [SerializeField] Material[] lightOnBase;
    [SerializeField] MeshRenderer[] lightRenderer;

    [SerializeField] HeldItem heldItemScript;

    int numOfPlacedCores = 0;
    public UnityEvent OpenLastDoor;
    public UnityEvent PlacedCore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void InteractedWith()
    {
        if (heldItemScript.hasItem)
        {
            ReactorCores[numOfPlacedCores].SetActive(true);
            lightRenderer[numOfPlacedCores].material = lightOnBase[numOfPlacedCores];
            numOfPlacedCores++;
            if (numOfPlacedCores >= 3)
            {
                OpenLastDoor.Invoke();
            }
            heldItemScript.hasItem = false;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
