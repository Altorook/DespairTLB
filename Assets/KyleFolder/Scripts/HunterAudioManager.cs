using UnityEngine;

public class HunterAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioList;
    [SerializeField]
    private AudioClip[] musicList;
    [SerializeField]
    private AudioSource SFXSource;
    [SerializeField]
    private AudioSource MusicSource;
    [SerializeField]
    public static HunterAudioManager HunterInstance;

    void Start()
    {
        
    }

    public void Awake()
    {
        HunterInstance = this;
    }
    public static void PlaySound(int sound)
    {
        HunterInstance.SFXSource.PlayOneShot(HunterInstance.audioList[sound]);
    }
    public static void PlayMusic(int sound)
    {
        HunterInstance.MusicSource.Stop();
        HunterInstance.MusicSource.clip = (HunterInstance.musicList[sound]);
        HunterInstance.MusicSource.Play();
    }
}
