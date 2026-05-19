using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Button testButton;
    private CharacterData characterDialogue;

    void Awake()
    {
        characterDialogue = GetComponent<CharacterData>();
    }

    void OnDialogueTrigger()
    {
        DialogueManager.instance.SetCurrentCharacter(characterDialogue);
        DialogueManager.instance.inDialog = true;
        DialogueManager.instance.ClearText();
        DialogueManager.instance.FadeUI(true, 0.2f, 0);
    }
    
    void OnEnable()
    {
        testButton.onClick.AddListener(() => OnDialogueTrigger());
    }

    void OnDisable()
    {
        testButton.onClick.RemoveAllListeners();
    }
}
