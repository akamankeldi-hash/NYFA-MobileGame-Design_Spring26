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
    [SerializeField] private GameObject bioWidget;
    [SerializeField] private GameObject inspectWidget;
    [SerializeField] private GameObject questioningWidget;
    [SerializeField] private GameObject characterModel;
    private RectTransform bioWidgetTransform;
    private RectTransform questioningWidgetTransform;

    [Header("Animation Variables")]
    [SerializeField] private float panelHiddenY = -1100f;
    [SerializeField] private float panelOpenY = 0f;

    [Header("Character Rotation")]
    [SerializeField] private InspectSystem inspectSystem;

    [Header("Toast")]
    [SerializeField] private GameObject propDiscoveredToast;
    [SerializeField] private TMPro.TextMeshProUGUI propToastText;

    private enum HudState { Inspect, BioOpen, Questioning }
    private HudState currentState = HudState.Inspect;
    private bool bioIsOpen = false;
    private bool questioningIsOpen = false;
    private bool isAnimating = false;

    void Awake()
    {
        instance = this;
        bioWidgetTransform = bioWidget.GetComponent<RectTransform>();
        questioningWidgetTransform = questioningWidget.GetComponent<RectTransform>();
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
        Debug.Log("Click Questioning?");
        if (isAnimating) return;

        if (questioningIsOpen)
        {
            CloseQuestioning();
        }
        else
        {
            OpenQuestioning();
        }
    }

    void EnterInspect(bool instant = false)
    {
        currentState = HudState.Inspect;

        inspectWidget.SetActive(true);
        // questioningWidget.SetActive(false);
        characterModel.SetActive(true);
        SetRotationEnabled(true);

        if (instant)
        {
            bioWidgetTransform.anchoredPosition = new Vector2(bioWidgetTransform.anchoredPosition.x, panelHiddenY);
            questioningWidgetTransform.anchoredPosition = new Vector2(questioningWidgetTransform.anchoredPosition.x, panelHiddenY);
        }
    }    

    void OpenBio()
    {
        isAnimating = true;
        currentState = HudState.BioOpen;
        bioIsOpen = true;

        inspectWidget.SetActive(false);
        // questioningWidget.SetActive(false);
        characterModel.SetActive(false);
        SetRotationEnabled(false);

        bioWidgetTransform.DOAnchorPosY(panelOpenY, 0.4f).SetEase(Ease.OutBack).OnComplete(() => isAnimating = false);
    }

    void CloseBio()
    {
        isAnimating = true;
        bioIsOpen = false;

        bioWidgetTransform.DOAnchorPosY(panelHiddenY, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            isAnimating = false;
            EnterInspect();
        });
    }

    void OpenQuestioning()
    {
        currentState = HudState.Questioning;
        isAnimating = true;
        questioningIsOpen = true;

        inspectWidget.SetActive(false);
        // questioningWidget.SetActive(true);
        characterModel.SetActive(false);
        SetRotationEnabled(false);

        questioningWidgetTransform.DOAnchorPosY(panelOpenY, 0.4f).SetEase(Ease.OutBack).OnComplete(() => isAnimating = false);
    }

    void CloseQuestioning()
    {
        questioningIsOpen = false;
        isAnimating = true;
        // questioningIsOpen = false;

        questioningWidgetTransform.DOAnchorPosY(panelHiddenY, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            isAnimating = false;
            EnterInspect();
        });
    }

    void SetRotationEnabled(bool enabled)
    {
        inspectSystem.ResetRotation();

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

    public GameObject GetActiveWidget() => currentState switch
    {
        HudState.BioOpen => bioWidget,
        HudState.Inspect => bioWidget,
        HudState.Questioning => questioningWidget,
        _ => null
    };
}