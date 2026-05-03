using DG.Tweening;
using Mono.Cecil;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public TMP_Animated animatedText;
    [SerializeField] private CharacterDialogue currentCharacter;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image nameBubble;
    [SerializeField] private TextMeshProUGUI nameTMP;

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
        currentCharacter.Reset();
        inDialog = false;
        canExit = false;
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
}