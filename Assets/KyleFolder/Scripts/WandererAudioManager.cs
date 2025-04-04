using UnityEngine;

public class WandererAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioList;
    [SerializeField]
    private AudioSource SFXSource;
    [SerializeField]
    private AudioSource WalkingSource;
    [SerializeField]
    public static WandererAudioManager WandererInstance;

    void Start()
    {

    }

    public void Awake()
    {
        WandererInstance = this;
    }
    public static void PlaySound(int sound)
    {
        WandererInstance.SFXSource.PlayOneShot(WandererInstance.audioList[sound]);
    }
    public static void PlayWalkingSound()
    {
        WandererInstance.WalkingSource.Play();
    }
    public static void StopWalkingSound()
    {
        WandererInstance.WalkingSource.Stop();
    }

}
