using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HudManager : MonoBehaviour
{
    public static HudManager instance;

    [Header("Nav Buttons")]
    [SerializeField] private Button bioButton;
    [SerializeField] private Button questioningButton;
    [SerializeField] private Button heavenButton;
    [SerializeField] private Button hellButton;

    [Header("Widgets")]
    [SerializeField] private GameObject inspectWidget;
    [SerializeField] private GameObject questioningWidget;

    [Header("Bio Panel")]
    [SerializeField] private RectTransform bioWidgetTransform;
    [SerializeField] private float bioHiddenY = -1100f;
    [SerializeField] private float bioOpenY   =  0f;

    [Header("Character Rotation")]
    [SerializeField] private InspectSystem inspectSystem;

    [Header("Toast")]
    [SerializeField] private GameObject propDiscoveredToast;
    [SerializeField] private TMPro.TextMeshProUGUI propToastText;

    private enum HudState { Inspect, BioOpen, Questioning }
    private HudState currentState = HudState.Inspect;
    private bool bioIsOpen = false;
    private bool isAnimating = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        bioButton.onClick.AddListener(OnBioButton);
        questioningButton.onClick.AddListener(OnQuestioningButton);
        heavenButton.onClick.AddListener(() => VerdictManager.instance.TryVerdict(true));
        hellButton.onClick.AddListener(() => VerdictManager.instance.TryVerdict(false));

        EnterInspect(instant: true);
    }

    void OnBioButton()
    {
        if (isAnimating) return;

        if (bioIsOpen)
        {
            CloseBio();
        }
        else
        {
            OpenBio();
        }
    }

    void OnQuestioningButton()
    {
        if (isAnimating) return;
        if (currentState == HudState.BioOpen) return;

        if (currentState == HudState.Questioning)
        {
            EnterInspect();
        }
        else
        {
            EnterQuestioning();
        }
    }

    void OpenBio()
    {
        isAnimating = true;
        currentState = HudState.BioOpen;
        bioIsOpen = true;

        inspectWidget.SetActive(false);
        questioningWidget.SetActive(false);
        SetRotationEnabled(false);

        bioWidgetTransform.DOAnchorPosY(bioOpenY, 0.4f).SetEase(Ease.OutBack).OnComplete(() => isAnimating = false);
    }

    void CloseBio()
    {
        isAnimating = true;
        bioIsOpen = false;

        bioWidgetTransform.DOAnchorPosY(bioHiddenY, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
            {
                isAnimating = false;
                EnterInspect();
            });
    }

    void EnterInspect(bool instant = false)
    {
        currentState = HudState.Inspect;

        inspectWidget.SetActive(true);
        questioningWidget.SetActive(false);
        SetRotationEnabled(true);

        if (instant)
        {
            bioWidgetTransform.anchoredPosition = new Vector2(bioWidgetTransform.anchoredPosition.x, bioHiddenY);
        }
    }

    void EnterQuestioning()
    {
        currentState = HudState.Questioning;

        inspectWidget.SetActive(false);
        questioningWidget.SetActive(true);
        SetRotationEnabled(false);
    }

    void SetRotationEnabled(bool enabled)
    {
        if (inspectSystem != null)
        {
            inspectSystem.enabled = enabled;
        }
    }

    public void ShowPropDiscoveredToast(E_PropType propType)
    {
        if (propDiscoveredToast == null) return;

        propToastText.text = $"New question unlocked!";
        propDiscoveredToast.SetActive(true);

        DOVirtual.DelayedCall(2f, () =>
        {
            if (propDiscoveredToast != null)
                propDiscoveredToast.SetActive(false);
        });
    }
}