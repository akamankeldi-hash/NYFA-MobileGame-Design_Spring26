using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HudManager : MonoBehaviour
{
    public static HudManager instance;

    [Header("Nav Buttons")]
    [SerializeField] private Button bioButton;
    [SerializeField] private Button questioningButton;
    [SerializeField] private Button heavenButton;
    [SerializeField] private Button hellButton;
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color inactiveColor = Color.gray;

    [Header("Widgets")]
    [SerializeField] private GameObject bioWidget;
    [SerializeField] private GameObject inspectWidget;
    [SerializeField] private GameObject questioningWidget;
    [SerializeField] private GameObject characterModel;
    [SerializeField] private GameObject finalScoreWidget;

    private RectTransform bioWidgetTransform;
    private RectTransform questioningWidgetTransform;

    [Header("Animation Variables")]
    [SerializeField] private float panelHiddenY = -1100f;
    [SerializeField] private float panelOpenY = 0f;
    [SerializeField] private float openDuration = 0.4f;
    [SerializeField] private float closeDuration = 0.3f;

    [Header("Character Rotation")]
    [SerializeField] private InspectSystem inspectSystem;

    private enum HudState { Inspect, BioOpen, Questioning }
    private HudState currentState = HudState.Inspect;
    private bool isAnimating = false;
    private bool navLocked = false;

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

    public void SetNavLocked(bool locked)
    {
        navLocked = locked;

        bioButton.interactable = !locked;
        questioningButton.interactable = !locked;
        heavenButton.interactable = !locked && VerdictManager.instance.CanJudge();
        if(heavenButton.interactable)
        {
            heavenButton.GetComponentInChildren<Image>().color = activeColor;
        }
        else
        {
            heavenButton.GetComponentInChildren<Image>().color = inactiveColor;
        }
        hellButton.interactable = !locked && VerdictManager.instance.CanJudge();
        if(hellButton.interactable)
        {
            hellButton.GetComponentInChildren<Image>().color = activeColor;
        }
        else
        {
            hellButton.GetComponentInChildren<Image>().color = inactiveColor;
        }
    }

    void OnBioButton()
    {
        if (isAnimating || navLocked) return;

        if (currentState == HudState.BioOpen)
        {
            ClosePanel(bioWidgetTransform, () => EnterInspect());
        }
        else if (currentState == HudState.Questioning)
        {
            ClosePanel(questioningWidgetTransform, () => OpenPanel(HudState.BioOpen, bioWidgetTransform));
        }
        else
        {
            OpenPanel(HudState.BioOpen, bioWidgetTransform);
        }
    }

    void OnQuestioningButton()
    {
        if (isAnimating || navLocked) return;

        if (currentState == HudState.Questioning)
        {
            ClosePanel(questioningWidgetTransform, () => EnterInspect());
        }
        else if (currentState == HudState.BioOpen)
        {
            ClosePanel(bioWidgetTransform, () => OpenPanel(HudState.Questioning, questioningWidgetTransform));
        }
        else
        {
            OpenPanel(HudState.Questioning, questioningWidgetTransform);
        }
    }

    void EnterInspect(bool instant = false)
    {
        currentState = HudState.Inspect;

        inspectWidget.SetActive(true);
        characterModel.SetActive(true);
        SetRotationEnabled(true);

        if (instant)
        {
            SetY(bioWidgetTransform, panelHiddenY);
            SetY(questioningWidgetTransform, panelHiddenY);
        }
    }

    void OpenPanel(HudState targetState, RectTransform panel)
    {
        isAnimating = true;
        currentState = targetState;

        inspectWidget.SetActive(false);
        characterModel.SetActive(false);
        SetRotationEnabled(false);

        panel.DOAnchorPosY(panelOpenY, openDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() => isAnimating = false);
    }

    void ClosePanel(RectTransform panel, System.Action onClosed)
    {
        isAnimating = true;

        panel.DOAnchorPosY(panelHiddenY, closeDuration)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                isAnimating = false;
                onClosed?.Invoke();
            });
    }

    void SetRotationEnabled(bool isEnabled)
    {
        if (inspectSystem == null) return;

        inspectSystem.ResetRotation();
        inspectSystem.enabled = isEnabled;
    }

    void SetY(RectTransform rect, float y)
    {
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, y);
    }

    public RectTransform GetActivePanelTransform() => currentState switch
    {
        HudState.BioOpen => bioWidgetTransform,
        HudState.Questioning => questioningWidgetTransform,
        _ => null
    };

    public float GetPanelHiddenY() => panelHiddenY;

    public GameObject GetCharacterModel() => characterModel;

    public void ForceCloseAll()
    {
        if (currentState == HudState.BioOpen)
            ClosePanel(bioWidgetTransform, () => EnterInspect());
        else if (currentState == HudState.Questioning)
            ClosePanel(questioningWidgetTransform, () => EnterInspect());
        else
            EnterInspect();
    }

    public void ShowFinalScore(int score)
    {
        finalScoreWidget.SetActive(true);
        finalScoreWidget.GetComponentInChildren<TextMeshProUGUI>().text = "Final Score: " + score;
    }
}