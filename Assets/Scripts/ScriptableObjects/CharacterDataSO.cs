using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "ScriptableObjects/Character Data")]
public class CharacterDataSO : ScriptableObject
{
    public string characterFirstName;
    public string characterLastName;
    public bool correctVerdictIsHeaven;

    // Traits
    public string traitJob;
    public string traitCauseOfDeath;
    public string traitLegacies;
    public string traitPersonality;
}