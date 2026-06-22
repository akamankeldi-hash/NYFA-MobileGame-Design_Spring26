using UnityEngine;

public class UiFollowTarget : MonoBehaviour
{
    [Header("Chain length range")]
    [SerializeField] private float maxLength = 1440f;
    [SerializeField] private float minLength = 300f;

    private RectTransform rectTransform;
    private RectTransform currentTarget;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        if (HudManager.instance == null) return;

        currentTarget = HudManager.instance.GetActivePanelTransform();

        float targetHeight = CalculateTargetHeight();
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight);
    }

    private float CalculateTargetHeight()
    {
        if (currentTarget == null)
            return maxLength;

        float hiddenY = HudManager.instance.GetPanelHiddenY();
        float panelY = currentTarget.anchoredPosition.y;

        float t = InverseLerpUnclamped(hiddenY, 0f, panelY);
        return LerpUnclamped(maxLength, minLength, t);
    }

    private float InverseLerpUnclamped(float a, float b, float value)
    {
        return (value - a) / (b - a);
    }

    private float LerpUnclamped(float a, float b, float t)
    {
        return a + (b - a) * t;
    }
}