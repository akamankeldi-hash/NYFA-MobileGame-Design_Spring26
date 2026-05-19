using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
        bioText.text = "Name: " + charDataSO.characterFirstName + " " +  charDataSO.characterLastName + " Job: " + charDataSO.traitJob;
    }
}
