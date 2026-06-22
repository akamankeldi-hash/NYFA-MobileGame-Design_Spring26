using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnlockableQuestions : MonoBehaviour
{
    public static UnlockableQuestions instance;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private CharacterData characterData;
    [SerializeField] QuestionOptionsSO questionOptions;
    [SerializeField] Button question1Button, question2Button, question3Button, question4Button, question5Button;
    [SerializeField] TextMeshProUGUI question1Text, question2Text, question3Text, question4Text, question5Text;

    void Awake() => instance = this;

    void Start()
    {
        if (!questionOptions) return;
        ResetState();
    }

    public void ResetState()
    {
        question1Button.enabled = true;
        question2Button.enabled = true;
        question3Button.gameObject.SetActive(false);
        question4Button.gameObject.SetActive(false);
        question5Button.gameObject.SetActive(false);
        
        question1Text.text = questionOptions.Question1Text;
        question2Text.text = questionOptions.Question2Text;
        question3Text.text = "Locked";
        question4Text.text = "Locked";
        question5Text.text = "Locked";
    }

    void OnQuestionButtonClicked(int index)
    {
        switch (index)
        {
            case 1:
            DialogueManager.instance.ReceiveAnAnswer(E_QuestionType.WhatHappenedBeforeYouGotHere, true, 0.2f, 0);
            if (characterData != null)
                characterData.RevealDeath();
            break;
            
            case 2:
            DialogueManager.instance.ReceiveAnAnswer(E_QuestionType.WhatKindOfLifeDidYouLive, true, 0.2f, 0);
            if (characterData != null)
                characterData.RevealPersonality();
            break;
            
            case 3:
            DialogueManager.instance.ReceiveAnAnswer(E_QuestionType.WhatWasYourJob, true, 0.2f, 0);
            break;

            case 4:
            DialogueManager.instance.ReceiveAnAnswer(E_QuestionType.WhatHappenedToYourClothes, true, 0.2f, 0);
            break;
            
            case 5:
            DialogueManager.instance.ReceiveAnAnswer(E_QuestionType.WhyDoYouHaveAnItem, true, 0.2f, 0);
            break;
        }
        
        VerdictManager.instance.RegisterQuestionAsked();
    }
        
    void OnEnable()
    {
        question1Button.onClick.AddListener(() => OnQuestionButtonClicked(1));
        question2Button.onClick.AddListener(() => OnQuestionButtonClicked(2));
        question3Button.onClick.AddListener(() => OnQuestionButtonClicked(3));
        question4Button.onClick.AddListener(() => OnQuestionButtonClicked(4));
        question5Button.onClick.AddListener(() => OnQuestionButtonClicked(5));
    }
    void OnDisable()
    {
        question1Button.onClick.RemoveAllListeners();
        question2Button.onClick.RemoveAllListeners();
        question3Button.onClick.RemoveAllListeners();
        question4Button.onClick.RemoveAllListeners();
        question5Button.onClick.RemoveAllListeners();
    }

    public void UnlockQuestion(int index, string propDisplayName = null)
    {
        switch (index)
        {
            case 1:
                question1Button.interactable = true;
                question1Text.text = questionOptions.Question1Text;
                break;
            case 2:
                question2Button.interactable = true;
                question2Text.text = questionOptions.Question2Text;
                break;
            case 3:
                question3Button.gameObject.SetActive(true);
                question3Text.text = questionOptions.Question3Text;
                break;
            case 4:
                question4Button.gameObject.SetActive(true);
                question4Text.text = questionOptions.Question4Text;
                break;
            case 5:
                question5Button.gameObject.SetActive(true);
                question5Text.text = string.IsNullOrEmpty(propDisplayName)
                    ? questionOptions.Question5Text
                    : string.Format(questionOptions.Question5Text, propDisplayName);
                break;
        }
    }
}