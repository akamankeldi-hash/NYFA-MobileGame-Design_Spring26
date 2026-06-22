using System.Collections.Generic;
using UnityEngine;

public class CharacterDataGeneration : MonoBehaviour
{
    public static CharacterDataGeneration instance;

    [Header("Template pool")]
    [SerializeField] private List<CharacterTemplateSO> templatePool;

    [Header("Scene components")]
    [SerializeField] private CharacterData characterData;
    [SerializeField] private CharacterAppearanceApplier appearanceApplier;

    private CharacterTemplateSO currentTemplate;

    void Awake() => instance = this;

    void Start() => GenerateCharacter();

    [ContextMenu("Regenerate Character")]
    public void GenerateCharacter()
    {
        if (templatePool == null || templatePool.Count == 0)
        {
            HudManager.instance.ShowFinalScore(VerdictManager.instance.GetScore());
            // Debug.LogError("[CharacterDataGeneration] Template pool is empty.");
            return;
        }

        int index = Random.Range(0, templatePool.Count);
        currentTemplate = templatePool[index];
        templatePool.RemoveAt(index);

        Apply(currentTemplate);
    }

    public void GenerateSpecific(CharacterTemplateSO template)
    {
        templatePool.Remove(template);
        currentTemplate = template;
        Apply(template);
    }

    public CharacterTemplateSO GetCurrentTemplate() => currentTemplate;

    private void Apply(CharacterTemplateSO t)
    {
        characterData.SetData(t);
        appearanceApplier.Apply(t);
        DialogueManager.instance.StartConversation(characterData);

        Debug.Log($"[Gen] {t.characterFirstName} {t.characterLastName} | {t.traitJob} | Sin: {t.characterSin} | Remaining: {templatePool.Count}");
    }
}