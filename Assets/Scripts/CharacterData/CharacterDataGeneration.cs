using UnityEngine;

public class CharacterDataGeneration : MonoBehaviour
{
    public static CharacterDataGeneration instance;
    [SerializeField] private CharacterDataSO[] characterPool;
    private int currentIndex = 0;

    void Awake() => instance = this;
    
    public CharacterDataSO GenerateNext()
    {
        var data = characterPool[currentIndex];
        currentIndex++;
        return data;
    }

    public static E_CharacterSins RollSin()
    {
        if (Random.value > 0.20f) return E_CharacterSins.None;
        return (E_CharacterSins)Random.Range(1,8);
    }
}
