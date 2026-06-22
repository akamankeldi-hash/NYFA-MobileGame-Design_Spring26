using UnityEngine;
using TMPro;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private CharacterTemplateSO characterTemplate;
    [SerializeField] private DialogueDataSO dialogueDataSO;
    [SerializeField] private AnswerOptionsSO answerOptionsSO;
    [SerializeField] private CharacterBio characterBio;

    public bool characterIsTalking;
    public E_CharacterSins characterSins => characterTemplate != null
        ? characterTemplate.characterSin
        : E_CharacterSins.None;

    public bool deathRevealed { get; set; } = false;
    public bool personalityRevealed { get; set; } = false;
    public bool jobRevealed { get; private set; } = false;

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

    public void SetData(CharacterTemplateSO template)
    {
        characterTemplate = template;
        deathRevealed = false;
        jobRevealed = false;
        var dialogueSO = ScriptableObject.CreateInstance<DialogueDataSO>();
        dialogueSO.conversationBlock = new System.Collections.Generic.List<string>(template.introLines);
        dialogueDataSO = dialogueSO;

        var answerSO = ScriptableObject.CreateInstance<AnswerOptionsSO>();
        answerSO.Answer1Text = Fallback(template.answerWhatHappenedBefore);
        answerSO.Answer2Text = Fallback(template.answerWhatKindOfLife);
        answerSO.Answer3Text = Fallback(template.answerWhatWasYourJob);
        answerSO.Answer4Text = Fallback(template.answerWhatHappenedClothes);
        answerSO.Answer5Text = Fallback(template.answerWhyHaveItem);
        answerOptionsSO = answerSO;

        if (characterBio != null)
        characterBio.SetBioInfo();
    }

    public void RevealDeath()
    {
        if (deathRevealed) return;
        deathRevealed = true;

        if (characterBio != null)
        characterBio.SetBioInfo();
    }

    public void RevealPersonality()
    {
        if (characterTemplate == null) return;
        personalityRevealed = true;
        
        if (characterBio != null)
        characterBio.SetBioInfo();
    }

    public void RevealJob()
    {
        if (jobRevealed) return;
        jobRevealed = true;

        if (characterBio != null)
        characterBio.SetBioInfo();
    }

    private string Fallback(string s) =>
        string.IsNullOrWhiteSpace(s) ? "..." : s;

    public CharacterTemplateSO GetCharacterTemplate() => characterTemplate;
    public DialogueDataSO GetDialogueDataSO() => dialogueDataSO;
    public AnswerOptionsSO GetAnswerOptionsSO() => answerOptionsSO;
    public DialogueAudio GetDialogueAudio() => dialogueAudio;
}