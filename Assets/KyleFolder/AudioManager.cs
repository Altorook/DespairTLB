using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource SFXSource;
    [SerializeField]
    private AudioSource MusicSource;
    [SerializeField]
    public static AudioManager Instance;

    public void Start()
    {
        
    }
    public void Awake()
    {
        Instance = this;
    }

    public static void PlaySound()
    {
        Instance.SFXSource.Play();
    }
}
