using UnityEngine;
using UnityEngine.Events;

public class PillarRotation : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int correctRotation;
    public UnityEvent SolvedPillar;
    public UnityEvent LostPillar;
    public int currentRotation;
    void Start()
    {
        SetRotation();
    }
    private void SetRotation()
    {
        correctRotation = Random.Range(0, 4);
        do
        {
            currentRotation = Random.Range(0, 4);
        } while(currentRotation == correctRotation);
        this.transform.rotation = Quaternion.Euler(0, currentRotation * 90, 0);
    }
    public void InteractedWith()
    {
        currentRotation++;
        if(currentRotation > 3)
        {
            currentRotation = 0;
        }
        this.transform.rotation = Quaternion.Euler(0, currentRotation * 90, 0);
        if(currentRotation == correctRotation)
        {
            SolvedPillar.Invoke();
        }
        else
        {
            LostPillar.Invoke();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
