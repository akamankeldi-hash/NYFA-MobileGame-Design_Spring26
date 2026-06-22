using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Button triggerButton;
    private CharacterData characterDialogue;

    void Awake()
    {
        characterDialogue = GetComponent<CharacterData>();
    }

    void OnDialogueTrigger()
    {
        DialogueManager.instance.StartConversation(characterDialogue);
    }
    
    void OnEnable()
    {
        triggerButton.onClick.AddListener(() => OnDialogueTrigger());
    }

    void OnDisable()
    {
        triggerButton.onClick.RemoveAllListeners();
    }
}