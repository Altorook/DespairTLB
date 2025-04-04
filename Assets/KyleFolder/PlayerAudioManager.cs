using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource WalkingSource;
    [SerializeField]
    public static PlayerAudioManager PlayerInstance;

    public void Awake()
    {
        PlayerInstance = this;
    }
    
    public static void PlayWalkingSound()
    {
        PlayerInstance.WalkingSource.Play();
    }
    public static void StopWalkingSound()
    {
        PlayerInstance.WalkingSource.Stop();
    }
}
