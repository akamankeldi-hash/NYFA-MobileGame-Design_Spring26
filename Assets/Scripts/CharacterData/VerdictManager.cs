using DG.Tweening;
using UnityEngine;

public class VerdictManager : MonoBehaviour
{
    private static VerdictManager _instance;
    public static VerdictManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindAnyObjectByType<VerdictManager>();
            return _instance;
        }
    }

    [SerializeField] private GameObject verdictLockedWarning;
    [SerializeField] private float exitDistance = 5f;
    [SerializeField] private float exitDuration = 0.5f;

    [Header("Scoring")]
    [SerializeField] private int score = 0;
    [SerializeField] private int correctVerdictPoints = 10;
    [SerializeField] private int wrongVerdictPoints = -5;

    private Transform characterModel;
    private Vector3 characterRestingPosition;
    private int questionsAsked = 0;
    private bool isAnimating = false;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        characterModel = HudManager.instance.GetCharacterModel().transform;
        characterRestingPosition = characterModel.localPosition;
        UpdateVerdictAvailability();
    }

    public void RegisterQuestionAsked()
    {
        questionsAsked++;
        Debug.Log("[VerdictManager] Question asked. Total: " + questionsAsked);
        UpdateVerdictAvailability();
    }

    private void UpdateVerdictAvailability()
    {
        if (verdictLockedWarning != null)
            verdictLockedWarning.SetActive(questionsAsked == 0);
    }

    public bool CanJudge() => questionsAsked > 0;

    public void TryVerdict(bool sentToHeaven)
    {
        if (questionsAsked == 0)
        {
            Debug.Log("[VerdictManager] Can't judge — no questions asked yet.");
            return;
        }

        if (isAnimating) return;
        isAnimating = true;

        HudManager.instance.ForceCloseAll();
        DialogueManager.instance.ForceClose();

        var template = CharacterDataGeneration.instance.GetCurrentTemplate();
        if (template != null)
        {
            SoundType soundToPlay = (sentToHeaven ? SoundType.VerdictHeaven : SoundType.VerdictHell);
            AudioManager.PlaySound(soundToPlay);
            bool wasCorrect = sentToHeaven == template.correctVerdictIsHeaven;
            score += wasCorrect ? correctVerdictPoints : wrongVerdictPoints;
            Debug.Log($"[VerdictManager] {(sentToHeaven ? "Heaven" : "Hell")} | {template.characterFirstName} {template.characterLastName} | {(wasCorrect ? "Correct" : "Wrong")} | Score: {score}");
        }

        float direction = sentToHeaven ? 1f : -1f;
        Vector3 exitPosition = characterRestingPosition + Vector3.right * direction * exitDistance;

        characterModel.DOLocalMove(exitPosition, exitDuration)
            .SetEase(Ease.InBack)
            .OnComplete(() => LoadNextSoul());
    }

    private void LoadNextSoul()
    {
        questionsAsked = 0;
        characterModel.localPosition = characterRestingPosition;

        UnlockableQuestions.instance.ResetState();
        CharacterDataGeneration.instance.GenerateCharacter();

        isAnimating = false;
        UpdateVerdictAvailability();
    }

    public int GetScore() => score;
}