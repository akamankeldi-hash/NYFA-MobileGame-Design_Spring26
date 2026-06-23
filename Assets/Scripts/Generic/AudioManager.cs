using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefSO audioClipRefSO;
    private AudioSource audioSource;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    public static AudioManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = sfxMixerGroup;
    }

    public static void PlaySound(SoundType sound)
    {
        AudioClip[] clips = sound switch
        {
            SoundType.None => null,
            SoundType.VerdictHeaven => Instance.audioClipRefSO.VerdictHeaven,
            SoundType.VerdictHell => Instance.audioClipRefSO.VerdictHell,
            _ => null
        };

        if (clips != null && clips.Length > 0)
        {
            Instance.audioSource.pitch = Random.Range(0.95f, 1.05f);
            
            AudioClip randomClip = clips[Random.Range(0, clips.Length)];
            Instance.audioSource.PlayOneShot(randomClip, Random.Range(0.95f, 1.05f));
        }
    }
}