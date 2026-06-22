using UnityEngine;

public class PropFeedbackFX : MonoBehaviour
{
    private static PropFeedbackFX _instance;
    public static PropFeedbackFX instance
    {
        get
        {
            if (_instance == null)
                _instance = FindAnyObjectByType<PropFeedbackFX>();
            return _instance;
        }
    }

    private ParticleSystem particles;

    void Awake()
    {
        _instance = this;
        particles = GetComponent<ParticleSystem>();
    }

    public void PlayAt(Vector3 position)
    {
        transform.position = position;
        particles.Play();
    }
}