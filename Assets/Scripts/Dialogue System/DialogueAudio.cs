using UnityEngine;
using TMPro;

public class DialogueAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] voices;
    [SerializeField] private AudioClip[] punctuations;
    [SerializeField] private AudioSource voiceSource;
    [SerializeField] private AudioSource punctuationSource;
    
    private CharacterData currentCharacter;
    private TMP_Animated animatedText;

    void Start()
    {
        currentCharacter = GetComponent<CharacterData>();
        animatedText = DialogueManager.instance.animatedText;
        animatedText.onTextReveal.AddListener((newChar) => ReproduceSound(newChar));
    }

    public void ReproduceSound(char c)
    {
        if (currentCharacter != DialogueManager.instance.GetCurrentCharacter()) return;
        
        if (char.IsPunctuation(c) && !punctuationSource.isPlaying)
        {
            voiceSource.Stop();
            punctuationSource.clip = punctuations[Random.Range(0, punctuations.Length)];
            punctuationSource.Play();
        }

        if (char.IsLetter(c) && !voiceSource.isPlaying)
        {
            punctuationSource.Stop();
            voiceSource.clip = voices[Random.Range(0, voices.Length)];
            voiceSource.Play();
        }
    }

    public void SetPitch(float pitch)
    {
        voiceSource.pitch = pitch;
        punctuationSource.pitch = pitch;
    }
}