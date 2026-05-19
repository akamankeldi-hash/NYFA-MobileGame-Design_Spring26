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
    
    public CharacterDataSO GetCharacterDataSO()
    {
        return characterDataSO;
    }

    public DialogueDataSO GetDialogueDataSO()
    {
        return dialogueDataSO;
    }

    public AnswerOptionsSO GetAnswerOptionsSO()
    {
        return answerOptionsSO;
    }
}
