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
    [SerializeField] private Image nameBubble;
    [SerializeField] private Button dialogueBubbleButton;
    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private GameObject unlockableQuestionsGO;
    [SerializeField] private E_GameplayUiState uiState;

    private int dialogueIndex;
    [Space (25)]
    public bool inDialog;
    [SerializeField] private bool canExit;
    [SerializeField] private bool nextDialogue;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        dialogueBubbleButton.onClick.AddListener(() => HideUI());
    }

    void Update()
    {
        if (inDialog)
        {
            bool advance = (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame);
            advance |= Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
            if (advance) NextDialogueLine();
        }
    }

    public void FadeUI(bool show, float time, float delay)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(delay);
        sequence.Append(canvasGroup.DOFade(show ? 1 : 0, time));
        if (show)
        {
            dialogueIndex = 0;
            sequence.Join(canvasGroup.transform.DOScale(0, time * 2).From().SetEase(Ease.OutBack));
            sequence.AppendCallback(() => animatedText.ReadText(currentCharacter.GetDialogueDataSO().conversationBlock[0]));
        }
    }

    public void NextDialogueLine()
    {
        if (canExit)
        {
            FadeUI(false, 0.2f, 0);
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(0.8f);
            sequence.AppendCallback(() => ResetState());
        }
        else if (nextDialogue)
        {
            animatedText.ReadText(currentCharacter.GetDialogueDataSO().conversationBlock[dialogueIndex]);
        }
    }

    public void FinishDialogue()
    {
        if(dialogueIndex < currentCharacter.GetDialogueDataSO().conversationBlock.Count - 1)
        {
            dialogueIndex++;
            nextDialogue = true;
        }
        else
        {
            nextDialogue = false;
            canExit = true;
        }
    }

    private void HideUI()
    {
        FadeUI(false, 0.2f, 0);
    }
    
    public void ClearText()
    {
        animatedText.text = string.Empty;
    }

    public void ResetState()
    {
        inDialog = false;
        canExit = false;
    }

    public void ReceiveAnAnswer(E_QuestionType questionType, bool show, float time, float delay)
    {    
        string answer = string.Empty;
           
        switch (questionType)
        {
            case E_QuestionType.WhatHappenedBeforeYouGotHere:
                answer = currentCharacter.GetAnswerOptionsSO().Answer1Text;
                break;
            
            case E_QuestionType.WhatKindOfLifeDidYouLive:
                answer = currentCharacter.GetAnswerOptionsSO().Answer2Text;
                break;
            
            case E_QuestionType.WhatWasYourJob:
                answer = currentCharacter.GetAnswerOptionsSO().Answer3Text;
                break;

            case E_QuestionType.WhatHappenedToYourClothes:
                answer = currentCharacter.GetAnswerOptionsSO().Answer4Text;
                break;
            
            case E_QuestionType.WhyDoYouHaveAKnife:
                answer = currentCharacter.GetAnswerOptionsSO().Answer5Text;
                break;
        }
        
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(delay);
        sequence.Append(canvasGroup.DOFade(show ? 1 : 0, time));
        if (show)
        {
            dialogueIndex = 0;
            sequence.Join(canvasGroup.transform.DOScale(0, time * 2).From().SetEase(Ease.OutBack));
            sequence.AppendCallback(() => animatedText.ReadText(answer));
        }
    }

    private void DialogueFadeOut()
    {
        // FadeUI(false, 1.0f, 0);
        unlockableQuestionsGO.SetActive(true);
    }

    public void OpenQuestions()
    {
        unlockableQuestionsGO.SetActive(true);
    }

    public void CloseQuestions()
    {
        unlockableQuestionsGO.SetActive(false);
    }

    public void SetCurrentCharacter(CharacterData characterDialogue)
    {
        currentCharacter = characterDialogue;
        nameTMP.text = characterDialogue.GetCharacterDataSO().characterFirstName + " " + characterDialogue.GetCharacterDataSO().characterLastName;
        var profile = SinModifier.instance.GetProfile(characterDialogue.characterSins);
        characterDialogue.GetDialogueAudio().SetPitch(profile.audioPitch);
    }

    public CharacterData GetCurrentCharacter()
    {
        return currentCharacter;
    }

    void OnEnable()
    {
        animatedText.onDialogueFinish.AddListener(DialogueFadeOut);
    }

    void OnDisable()
    {
        animatedText.onDialogueFinish.RemoveAllListeners();
    }
}