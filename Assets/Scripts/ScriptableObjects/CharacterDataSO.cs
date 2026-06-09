using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "ScriptableObjects/Character Data")]
public class CharacterDataSO : ScriptableObject
{
    public string characterFirstName;
    public string characterLastName;
    [Range(0, 100)]public int traitAge;
    
    // Traits
    public string traitJob;
    public string traitCauseOfDeath;
    public string traitPersonality;
    public string traitLegacies;
    
    public E_CharacterSins characterSin;
    public E_PropType[] activeProp;

    public bool correctVerdictIsHeaven;
}