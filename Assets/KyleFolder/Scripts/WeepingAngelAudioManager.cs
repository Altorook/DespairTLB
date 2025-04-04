using UnityEngine;

public class WeepingAngelAudioManager : MonoBehaviour
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
    public static WeepingAngelAudioManager WeepingAngelInstance;

    void Start()
    {

    }

    public void Awake()
    {
        WeepingAngelInstance = this;
    }
    public static void PlaySound(int sound)
    {
        WeepingAngelInstance.SFXSource.PlayOneShot(WeepingAngelInstance.audioList[sound]);
    }
    public static void PlayMusic(int sound)
    {
        WeepingAngelInstance.MusicSource.clip = (WeepingAngelInstance.musicList[sound]);
        WeepingAngelInstance.MusicSource.Play();
    }
    public static void StopMusic(int sound)
    {
        WeepingAngelInstance.MusicSource.clip = (WeepingAngelInstance.musicList[sound]);
        WeepingAngelInstance.MusicSource.Stop();
    }
}
