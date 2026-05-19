using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public TMP_Animated animatedText;
    [SerializeField] private CharacterDialogue currentCharacter;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image nameBubble;
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
        animatedText.onDialogueFinish.AddListener(() => FinishDialogue());
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && inDialog)
        {
            NextDialogueLine();
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
            sequence.AppendCallback(() => animatedText.ReadText(currentCharacter.DialogueData.conversationBlock[0]));
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
        if (nextDialogue)
        {
            animatedText.ReadText(currentCharacter.DialogueData.conversationBlock[dialogueIndex]);
        }
    }

    public void FinishDialogue()
    {
        if(dialogueIndex < currentCharacter.DialogueData.conversationBlock.Count - 1)
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
                answer = currentCharacter.GetAnswerOptions().Answer1Text;
                break;
            
            case E_QuestionType.WhatKindOfLifeDidYouLive:
                answer = currentCharacter.GetAnswerOptions().Answer2Text;
                break;
            
            case E_QuestionType.WhatWasYourJob:
                answer = currentCharacter.GetAnswerOptions().Answer3Text;
                break;

            case E_QuestionType.WhatHappenedToYourClothes:
                answer = currentCharacter.GetAnswerOptions().Answer4Text;
                break;
            
            case E_QuestionType.WhyDoYouHaveAKnife:
                answer = currentCharacter.GetAnswerOptions().Answer5Text;
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

    public void SetCurrentCharacter(CharacterDialogue characterDialogue)
    {
        currentCharacter = characterDialogue;
        nameTMP.text = characterDialogue.GetCharacterData().characterFirstName + " " + characterDialogue.GetCharacterData().characterLastName;
    }

    public CharacterDialogue GetCurrentCharacter()
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