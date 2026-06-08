using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VerdictManager : MonoBehaviour
{
    public static VerdictManager instance;

    [SerializeField] private Button heavenButton;
    [SerializeField] private Button hellButton;
    [SerializeField] private GameObject verdictLockedWarning;
    [SerializeField] private GameObject characterModel;
    [SerializeField] private RectTransform characterCard;
    private int questionsAsked = 0;

    void Awake() => instance = this;

    public void RegisterQuestionAsked()
    {
        questionsAsked++;
        UpdateVerdictAvailabitlity();
    }

    private void UpdateVerdictAvailabitlity()
    {
        bool canJudge = questionsAsked > 0;
        heavenButton.interactable = canJudge;
        hellButton.interactable = canJudge;
        if (verdictLockedWarning) verdictLockedWarning.SetActive(!canJudge);
    }

    public void TryVerdict(bool heaven)
    {
        // if (questionsAsked == 0) return;
        Debug.Log("CLICK TEST");
        float targetX = heaven ? 900f : -900f;
        
        characterModel.transform.DOFlip();
        if (characterCard == null) return;
        characterCard.DOAnchorPosX(targetX, 0.4f).SetEase(Ease.InBack).OnComplete(() => LoadNextSoul());
    }

    private void LoadNextSoul()
    {
        questionsAsked = 0;
        UnlockableQuestions.instance.ResetState();
    }
}