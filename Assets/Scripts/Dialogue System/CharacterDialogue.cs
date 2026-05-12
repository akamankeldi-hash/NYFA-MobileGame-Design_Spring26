using UnityEngine;
using TMPro;

public class CharacterDialogue : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private AnswerOptions answerOptions;
    public bool characterIsTalking;
    public E_CharacterSins characterSins {get; private set;}
    private TMP_Animated animatedText;
    private DialogueAudio dialogueAudio;

    // Public Getters
    public DialogueData DialogueData => dialogueData;

    void Awake()
    {
        dialogueAudio = GetComponent<DialogueAudio>();
    }

    void Start()
    {
        animatedText = DialogueManager.instance.animatedText;
    }
    
    public CharacterData GetCharacterData()
    {
        return characterData;
    }

    public DialogueData GetDialogueData()
    {
        return dialogueData;
    }

    public AnswerOptions GetAnswerOptions()
    {
        return answerOptions;
    }
}
