using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterTemplate", menuName = "ScriptableObjects/Character Template")]
public class CharacterTemplateSO : ScriptableObject
{
    [Header("Identity")]
    public string characterFirstName;
    public string characterLastName;

    [Header("Traits")]
    public string traitJob;
    public string traitcauseOfDeath;
    public string traitPersonality;

    [Header("Sin")]
    public E_CharacterSins characterSin;

    [Header("Verdict")]
    public bool correctVerdictIsHeaven;

    public E_PropType[] activeProps;
}
