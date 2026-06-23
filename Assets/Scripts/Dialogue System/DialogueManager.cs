using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TouchPhase = UnityEngine.TouchPhase;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public TMP_Animated animatedText;
    [SerializeField] private CharacterData currentCharacter;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button dialogueBubbleButton;
    [SerializeField] private TextMeshProUGUI nameTMP;

    [Header("Text Settings")]
    [SerializeField] private float textSpeed = 10f;
    [SerializeField] private float autoDismissDelay = 3f;

    private int dialogueIndex;
    private bool inDialog;
    private bool canExit;
    private bool nextDialogue;
    private Tween autoDismissTween;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animatedText.speed = textSpeed;
        dialogueBubbleButton.onClick.AddListener(OnBubbleClicked);
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void Update()
    {
        if (!inDialog) return;

        bool advance = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
        advance |= Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        if (advance) NextDialogueLine();
    }

    // ── Public entry points ─────────────────────────────────────────

    public void StartConversation(CharacterData character)
    {
        Debug.Log("Dialogue Started");
        SetCurrentCharacter(character);
        inDialog = true;
        dialogueIndex = 0;
        canExit = false;
        nextDialogue = false;

        ClearText();
        ShowBubble(() => animatedText.ReadText(currentCharacter.GetDialogueDataSO().conversationBlock[0]));
        HudManager.instance.SetNavLocked(true);
    }

    public void ReceiveAnAnswer(E_QuestionType questionType, bool show, float time, float delay)
    {
        string answer = questionType switch
        {
            E_QuestionType.WhatHappenedBeforeYouGotHere => currentCharacter.GetAnswerOptionsSO().Answer1Text,
            E_QuestionType.WhatKindOfLifeDidYouLive     => currentCharacter.GetAnswerOptionsSO().Answer2Text,
            E_QuestionType.WhatWasYourJob               => currentCharacter.GetAnswerOptionsSO().Answer3Text,
            E_QuestionType.WhatHappenedToYourClothes    => currentCharacter.GetAnswerOptionsSO().Answer4Text,
            E_QuestionType.WhyDoYouHaveAnItem           => currentCharacter.GetAnswerOptionsSO().Answer5Text,
            _ => "..."
        };

        CancelAutoDismiss();

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(delay);
        if (show)
        {
            sequence.AppendCallback(() =>
            {
                inDialog = true;
                canExit = false;
                ShowBubble(() => animatedText.ReadText(answer));
            });
        }
        else
        {
            sequence.Append(canvasGroup.DOFade(0f, time)
                .OnComplete(() => SetBubbleInteractable(false)));
        }
    }

    // ── Dialogue flow ───────────────────────────────────────────────

    void NextDialogueLine()
    {
        if (canExit)
        {
            ForceClose();
        }
        else if (nextDialogue)
        {
            nextDialogue = false;
            CancelAutoDismiss();
            animatedText.ReadText(currentCharacter.GetDialogueDataSO().conversationBlock[dialogueIndex]);
        }
    }

    void OnLineFinished()
    {
        if (dialogueIndex < currentCharacter.GetDialogueDataSO().conversationBlock.Count - 1)
        {
            dialogueIndex++;
            nextDialogue = true;
        }
        else
        {
            canExit = true;
            ScheduleAutoDismiss();
        }
    }

    void OnAnswerFinished()
    {
        ScheduleAutoDismiss();
    }

    void ScheduleAutoDismiss()
    {
        CancelAutoDismiss();
        autoDismissTween = DOVirtual.DelayedCall(autoDismissDelay, () => ForceClose());
    }

    void CancelAutoDismiss()
    {
        autoDismissTween?.Kill();
        autoDismissTween = null;
    }

    void OnBubbleClicked()
    {
        if (!inDialog) return;

        if (nextDialogue)
        {
            NextDialogueLine();
        }
        else
        {
            ForceClose();
        }
    }

    public void ForceClose()
    {
        CancelAutoDismiss();
        HideBubble(() => ResetState());
    }

    // ── Show / hide ─────────────────────────────────────────────────

    void ShowBubble(System.Action onShown = null)
    {
        SetBubbleInteractable(true);
        DOTween.Kill(canvasGroup);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1f, 0.2f));
        sequence.Join(canvasGroup.transform.DOScale(0f, 0.4f).From().SetEase(Ease.OutBack));
        if (onShown != null)
            sequence.AppendCallback(() => onShown.Invoke());
    }

    void HideBubble(System.Action onHidden = null)
    {
        DOTween.Kill(canvasGroup);
        canvasGroup.DOFade(0f, 0.2f).OnComplete(() =>
        {
            SetBubbleInteractable(false);
            onHidden?.Invoke();
        });
    }

    void SetBubbleInteractable(bool active)
    {
        canvasGroup.interactable = active;
        canvasGroup.blocksRaycasts = active;
    }

    void ClearText()
    {
        animatedText.text = string.Empty;
    }

    void ResetState()
    {
        inDialog = false;
        canExit = false;
        nextDialogue = false;
        HudManager.instance.SetNavLocked(false);
    }

    void SetCurrentCharacter(CharacterData characterDialogue)
    {
        currentCharacter = characterDialogue;
        var template = characterDialogue.GetCharacterTemplate();
        nameTMP.text = template.characterFirstName + " " + template.characterLastName;

        if (SinModifier.instance != null)
        {
            var profile = SinModifier.instance.GetProfile(characterDialogue.characterSins);
            var audio = characterDialogue.GetDialogueAudio();
            if (audio != null)
                audio.SetPitch(profile.audioPitch);
        }
    }

    public CharacterData GetCurrentCharacter() => currentCharacter;

    // ── Event subscriptions ─────────────────────────────────────────

    void OnEnable()
    {
        animatedText.onDialogueFinish.AddListener(OnLineFinished);
    }

    void OnDisable()
    {
        animatedText.onDialogueFinish.RemoveAllListeners();
    }
}