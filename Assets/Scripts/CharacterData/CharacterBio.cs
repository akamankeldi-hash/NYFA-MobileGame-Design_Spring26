using TMPro;
using UnityEngine;

public class CharacterBio : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bioText;
    [SerializeField] private CharacterData charData;
    private CharacterDataSO charDataSO;

    void Start()
    {
        SetBioInfo();
    }

    public void SetBioInfo()
    {
        charDataSO = charData.GetCharacterDataSO();
        bioText.text = "Name: " + charDataSO.characterFirstName + " " + charDataSO.characterLastName +
        "\nAge: " + charDataSO.traitAge +
        "\nPersonality: " + charDataSO.traitPersonality +
        "\nCause of death: " + charDataSO.traitCauseOfDeath +
        "\nJob: " + charDataSO.traitJob;
    }
}
