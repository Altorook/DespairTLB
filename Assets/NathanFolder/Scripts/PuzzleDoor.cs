using UnityEngine;
using System.Collections;

public class PuzzleDoor : MonoBehaviour
{
    float amountOpen = 0;
    [SerializeField] float amountToOpen;
    [SerializeField]
    float openSpeed;
    bool hasPlayedSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void OpenDoor()
    {
        if (!hasPlayedSound)
        {
            AudioManager.PlaySound(6);
            hasPlayedSound = true;
        }
        if (amountOpen < amountToOpen)
        {
            amountOpen += openSpeed;
            this.transform.Rotate(new Vector3(0, 0, openSpeed));
            StartCoroutine(SmoothOpen());
        }
    }
    IEnumerator SmoothOpen()
    {
        yield return new WaitForFixedUpdate();
        OpenDoor();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
