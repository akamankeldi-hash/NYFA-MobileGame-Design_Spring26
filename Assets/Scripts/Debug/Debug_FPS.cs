using TMPro;
using UnityEngine;

public class Debug_FPS : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;
    float pollingTime = 0.5f;

    private float timeAccumulator = 0f;
    private int frameCount = 0;

    void Update()
    {
        timeAccumulator += Time.unscaledDeltaTime;
        frameCount++;

        if(timeAccumulator >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / timeAccumulator);
            if(fpsText != null)
            {
                fpsText.text = $"{frameRate} FPS";
            }
        
        timeAccumulator = 0f;
        frameCount = 0;
        }
    }
}