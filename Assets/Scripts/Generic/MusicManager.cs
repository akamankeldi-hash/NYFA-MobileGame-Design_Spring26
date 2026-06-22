using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [Header("Audio Sources")]
    private AudioSource musicSource;

    [Header("Settings")]
    public float fadeDuration = 1.5f;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        if (musicSource == null) return;
        
        musicSource.Play();
        musicSource.volume = 1f;
    }
}