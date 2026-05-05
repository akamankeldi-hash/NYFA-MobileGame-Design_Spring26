using UnityEngine;
using TMPro;

public class CharacterDialogue : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private DialogueData dialogueData;
    public bool characterIsTalking;
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

    void Update()
    {
        
    }
    
    public void Reset()
    {
        
    }

    public CharacterData GetCharacterData()
    {
        return characterData;
    }

    public DialogueData GetDialogueData()
    {
        return dialogueData;
    }
}
