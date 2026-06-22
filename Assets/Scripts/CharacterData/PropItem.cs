using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PropItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private E_PropPiece propType;
    [SerializeField] private GameObject glowIndicator;
    [SerializeField] private float punchScale = 0.025f;
    [SerializeField] private float punchDuration = 0.3f;
    [SerializeField] private int punchVibrato = 5;
    [SerializeField] private float punchElasticity = 0.5f;

    private CharacterData characterData;
    private Collider clickCollider;
    private E_UnlockTarget unlockTarget = E_UnlockTarget.None;
    private bool hasBeenTapped = false;
    private bool isInteractable = false;

    void Awake()
    {
        characterData = GetComponentInParent<CharacterData>();
        clickCollider = GetComponent<Collider>();
    }

    public void SetUnlockTarget(E_UnlockTarget target)
    {
        unlockTarget = target;
        isInteractable = unlockTarget != E_UnlockTarget.None;

        if (clickCollider != null)
            clickCollider.enabled = isInteractable;
    }

    void OnEnable()
    {
        hasBeenTapped = false;

        if (glowIndicator != null)
            glowIndicator.SetActive(isInteractable);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isInteractable || hasBeenTapped) return;
        hasBeenTapped = true;

        if (glowIndicator != null)
            glowIndicator.SetActive(false);

        PropFeedbackFX.instance.PlayAt(transform.position);

        int questionIndex = (int)unlockTarget;
        UnlockableQuestions.instance.UnlockQuestion(questionIndex, PropDisplayNames.Get(propType));

        if (unlockTarget == E_UnlockTarget.WhatWasYourJob && characterData != null)
            characterData.RevealJob();

        transform.DOPunchScale(Vector3.one * punchScale, punchDuration, punchVibrato, punchElasticity);
    }
}