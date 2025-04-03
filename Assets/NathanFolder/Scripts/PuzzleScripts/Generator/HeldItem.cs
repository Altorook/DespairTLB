using UnityEngine;

public class HeldItem : MonoBehaviour
{
    public bool hasItem = false;
    [SerializeField] GameObject reactorItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void PickupItem()
    {
        reactorItem.SetActive(true);
       hasItem = true;
    }
    public void PlaceItem()
    {
        reactorItem.SetActive(false);
        hasItem = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
