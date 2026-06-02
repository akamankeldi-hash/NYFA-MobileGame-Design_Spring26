using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class HudManager : MonoBehaviour
{
    [SerializeField] private Button heavenButton, hellButton, bioButton, inspectButton, questioningButton;
    [SerializeField] private GameObject bioWidget, inspectWidget, questioningWidget;
    [SerializeField] private RectTransform bioWidgetTransform;
    private bool openBio;

    void Start()
    {
        bioButton.onClick.AddListener(() => BioButton());
        inspectButton.onClick.AddListener(() => InspectButton());
        questioningButton.onClick.AddListener(() => QuestionButton());
        heavenButton.onClick.AddListener(() => HeavenButton());
        hellButton.onClick.AddListener(() => HellButton());
        // bioButton.interactable = false;
    }

    private void HeavenButton()
    {
        VerdictManager.instance.TryVerdict(true);
    }

    private void HellButton()
    {
        VerdictManager.instance.TryVerdict(false);
    }

    void BioButton()
    {
        Debug.Log("BIO CLICK TEST");
        openBio = !openBio;
        OpenBio(openBio);
        // SwitchButtonInteraction();
        // bioButton.interactable = false;
        // bioWidget.SetActive(true);
    }

    void FillBio()
    {
        
    }

    void InspectButton()
    {
        SwitchButtonInteraction();
        // inspectButton.interactable = false;

        bioWidget.SetActive(false);
        inspectWidget.SetActive(true);
        questioningWidget.SetActive(false);
    }

    void QuestionButton()
    {
        SwitchButtonInteraction();
        // questioningButton.interactable = false;

        bioWidget.SetActive(false);
        inspectWidget.SetActive(false);
        questioningWidget.SetActive(true);
    }

    void SwitchButtonInteraction()
    {
        if (!bioButton.interactable)
        {
            // bioButton.interactable = true;
        }

        if (!inspectButton.interactable)
        {
            // inspectButton.interactable = true;
        }

        if (!questioningButton.interactable)
        {
            // questioningButton.interactable = true;
        }
    }

    void OpenBio(bool open)
    {
        inspectWidget.SetActive(false);
        questioningWidget.SetActive(false);
        
        float targetY = open ? 1100 : 0;
        bioWidgetTransform.DOAnchorPosY(targetY, 0.4f).SetEase(Ease.InBack).OnComplete(() => FillBio());
    }
}
