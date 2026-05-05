using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockableQuestions : MonoBehaviour
{
    [SerializeField] QuestionOptions questionOptions;
    [SerializeField] Button question1Button, question2Button, question3Button, question4Button, question5Button;
    [SerializeField] TextMeshProUGUI question1Text, question2Text, question3Text, question4Text, question5Text;
    
    void Start()
    {
        if (!questionOptions) return;
        ResetState();
    }

    void ResetState()
    {
        question1Button.enabled = true;
        question2Button.enabled = true;
        question3Button.interactable = false;
        question4Button.interactable = false;
        question5Button.interactable = false;
        
        question1Text.text = questionOptions.Question1Text;
        question2Text.text = questionOptions.Question2Text;
        question3Text.text = "Locked";
        question4Text.text = "Locked";
        question5Text.text = "Locked";
    }

    public void UnlockQuestion(int index)
    {
        switch (index)
        {
            case 1:
            break;
            
            case 2:
            break;
            
            case 3:
            question3Button.interactable = true;
            question3Text.text = questionOptions.Question3Text;
            break;

            case 4:
            question4Button.interactable = true;
            question4Text.text = questionOptions.Question4Text;
            break;
            
            case 5:
            question5Button.interactable = true;
            question5Text.text = questionOptions.Question5Text;
            break;
        }
    }
}