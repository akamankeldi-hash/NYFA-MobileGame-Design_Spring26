using UnityEngine;
using UnityEngine.UI;

public class DialogueTesting : MonoBehaviour
{
    [SerializeField] private Button testButton;
    private CharacterDialogue characterDialogue;

    void Awake()
    {
        characterDialogue = GetComponent<CharacterDialogue>();
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        testButton.onClick.AddListener(() => OnTest());
    }

    void OnDisable()
    {
        testButton.onClick.RemoveAllListeners();
    }

    void OnTest()
    {
        DialogueManager.instance.SetCurrentCharacter(characterDialogue);
        DialogueManager.instance.inDialog = true;
        DialogueManager.instance.ClearText();
        DialogueManager.instance.FadeUI(true, 0.2f, 0);
    }
}