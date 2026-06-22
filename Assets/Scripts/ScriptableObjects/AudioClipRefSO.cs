using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipRefSO", menuName = "ScriptableObjects/AudioClipRefSO")]
public class AudioClipRefSO : ScriptableObject
{
    public AudioClip[] VerdictHeaven;
    public AudioClip[] VerdictHell;
}