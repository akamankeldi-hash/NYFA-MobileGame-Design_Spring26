using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Template", menuName = "ScriptableObjects/Character Template")]
public class CharacterTemplateSO : ScriptableObject
{
    [System.Serializable]
    public struct PropUnlockEntry
    {
        public E_PropPiece prop;
        public E_UnlockTarget unlockTarget;
    }

    [System.Serializable]
    public struct PropMaterialOverride
    {
        public E_PropPiece prop;
        public Material material;
    }

    [Header("Identity")]
    public string characterFirstName;
    public string characterLastName;
    [Range(0, 123)] public int characterAge;

    [Header("Traits")]
    public string traitJob;
    public string traitCauseOfDeath;
    public string traitPersonality;

    [Header("Sin")]
    public E_CharacterSins characterSin;

    [Header("Verdict")]
    public bool correctVerdictIsHeaven;

    [Header("Outfit — pick one of each")]
    public E_UpperBody upperBody;
    public E_LowerBody lowerBody;
    
    [Header("Face")]
    public E_Face face;

    [Header("Material override per slot — leave null for default")]
    public Material upperBodyMaterial;
    public Material lowerBodyMaterial;

    [Header("Props — active prop paired with the question it unlocks")]
    public List<PropUnlockEntry> activeProps;
    public List<PropMaterialOverride> propMaterialOverrides;

    [Header("Intro dialogue")]
    [TextArea(3, 5)]
    public List<string> introLines;

    [Header("Answers")]
    [TextArea(3, 5)] public string answerWhatHappenedBefore;
    [TextArea(3, 5)] public string answerWhatKindOfLife;
    [TextArea(3, 5)] public string answerWhatWasYourJob;
    [TextArea(3, 5)] public string answerWhatHappenedClothes;
    [TextArea(3, 5)] public string answerWhyHaveItem;
}