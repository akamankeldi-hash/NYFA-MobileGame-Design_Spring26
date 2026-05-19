using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    [SerializeField] private Button bioButton, inspectButton, questioningButton;
    [SerializeField] private GameObject bioWidget, inspectWidget, questioningWidget;

    void Start()
    {
        bioButton.onClick.AddListener(() => BioButton());
        inspectButton.onClick.AddListener(() => InspectButton());
        questioningButton.onClick.AddListener(() => QuestionButton());
        bioButton.interactable = false;
    }

    void BioButton()
    {
        SwitchButtonInteraction();
        bioButton.interactable = false;

        bioWidget.SetActive(true);
        inspectWidget.SetActive(false);
        questioningWidget.SetActive(false);
    }

    void InspectButton()
    {
        SwitchButtonInteraction();
        inspectButton.interactable = false;

        bioWidget.SetActive(false);
        inspectWidget.SetActive(true);
        questioningWidget.SetActive(false);
    }

    void QuestionButton()
    {
        SwitchButtonInteraction();
        questioningButton.interactable = false;

        bioWidget.SetActive(false);
        inspectWidget.SetActive(false);
        questioningWidget.SetActive(true);
    }

    void SwitchButtonInteraction()
    {
        if (!bioButton.interactable)
        {
            bioButton.interactable = true;
        }

        if (!inspectButton.interactable)
        {
            inspectButton.interactable = true;
        }

        if (!questioningButton.interactable)
        {
            questioningButton.interactable = true;
        }
    }
}
