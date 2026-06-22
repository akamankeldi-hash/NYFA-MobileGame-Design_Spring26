using TMPro;
using UnityEngine;

public class CharacterBio : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bioText;
    [SerializeField] private CharacterData charData;
    private CharacterTemplateSO template;

    public void SetBioInfo()
    {
        template = charData.GetCharacterTemplate();
        if (template == null) return;

        string deathLine = charData.deathRevealed ? template.traitCauseOfDeath : "???";
        string personalityLine = charData.personalityRevealed ? template.traitPersonality : "???";
        string jobLine = charData.jobRevealed ? template.traitJob : "???";

        bioText.text = "Name: " + template.characterFirstName + " " + template.characterLastName +
        "\nAge: " + template.characterAge +
        "\nPersonality: " + personalityLine +
        "\nCause of death: " + deathLine +
        "\nJob: " + jobLine;
    }
}