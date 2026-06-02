using UnityEngine;
using TMPro;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private CharacterDataSO characterDataSO;
    [SerializeField] private DialogueDataSO dialogueDataSO;
    [SerializeField] private AnswerOptionsSO answerOptionsSO;
    [SerializeField] private CharacterBio characterBio;
    public bool characterIsTalking;
    public E_CharacterSins characterSins {get; private set;}
    
    private TMP_Animated animatedText;
    private DialogueAudio dialogueAudio;

    void Awake()
    {
        dialogueAudio = GetComponent<DialogueAudio>();
    }

    void Start()
    {
        animatedText = DialogueManager.instance.animatedText;
    }
    
    public CharacterDataSO GetCharacterDataSO() => characterDataSO;

    public DialogueDataSO GetDialogueDataSO() => dialogueDataSO;

    public AnswerOptionsSO GetAnswerOptionsSO() => answerOptionsSO;

    public DialogueAudio GetDialogueAudio() => dialogueAudio;
}
