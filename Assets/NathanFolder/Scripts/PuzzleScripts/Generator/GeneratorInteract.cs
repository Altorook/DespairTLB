using UnityEngine;

public class GeneratorInteract : MonoBehaviour, IInteractable
{
    bool hasBeenOpened;
    [SerializeField] GameObject reactorOnGenerator;
    [SerializeField] GameObject brokenWire;
    [SerializeField] GameObject standardWire;
    [SerializeField] HeldItem heldItemScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void InteractedWith()
    {
        if (!hasBeenOpened && !heldItemScript.hasItem)
        {
            AudioManager.PlaySound(5);
            this.transform.parent.transform.GetChild(4).gameObject.GetComponent<AudioSource>().loop = false;
            this.transform.parent.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Stop();

            reactorOnGenerator.SetActive(false);
            standardWire.SetActive(false);
            brokenWire.SetActive(true);
            heldItemScript.PickupItem();
            hasBeenOpened = true;
            heldItemScript.hasItem = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
