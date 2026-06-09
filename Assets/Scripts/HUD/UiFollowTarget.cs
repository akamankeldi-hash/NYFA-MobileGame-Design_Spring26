using UnityEngine;

public class UiFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;
    // [SerializeField] private 

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void UpdateFollowTarget()
    {
        if (targetToFollow == null || targetToFollow != HudManager.instance.GetActiveWidget().transform)
        {
            targetToFollow = HudManager.instance.GetActiveWidget().transform;
        }
    }

    void LateUpdate()
    {
        UpdateFollowTarget();
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1440.0f + targetToFollow.GetComponent<RectTransform>().position.y);

    }
}