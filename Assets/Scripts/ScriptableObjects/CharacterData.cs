using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "ScriptableObjects/Character Data")]
public class CharacterData : ScriptableObject
{
    public string characterFirstName;
    public string characterLastName;

    // Traits
    public string traitJob;
    public string traitCauseOfDeath;
    public string traitLegacies;
    public string traitPersonality;
}