using UnityEngine;

public class SinModifier : MonoBehaviour
{
    private static SinModifier _instance;
    public static SinModifier instance
    {
        get
        {
            if (_instance == null)
                _instance = FindAnyObjectByType<SinModifier>();
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    public struct SinProfile
    {
        public float audioPitch;
        public float dialogueSpeed;
        public string tonePrefix;
    }

    public SinProfile GetProfile(E_CharacterSins sin)
    {
        return sin switch
        {
            E_CharacterSins.Wrath => new SinProfile { audioPitch = 1.3f, dialogueSpeed = 18f, tonePrefix = "<speed=18>" },
            E_CharacterSins.Greed => new SinProfile { audioPitch = 0.9f, dialogueSpeed = 10f, tonePrefix = "<speed=10>" },
            E_CharacterSins.Sloth => new SinProfile { audioPitch = 0.7f, dialogueSpeed = 6f, tonePrefix = "<speed=6>" },
            E_CharacterSins.Pride => new SinProfile { audioPitch = 1.1f, dialogueSpeed = 11f, tonePrefix = "<speed=11>" },
            E_CharacterSins.Gluttony => new SinProfile { audioPitch = 0.8f, dialogueSpeed = 8f, tonePrefix = "<speed=8>" },
            E_CharacterSins.Envy => new SinProfile { audioPitch = 1.0f, dialogueSpeed = 12f, tonePrefix = "<speed=12>" },
            E_CharacterSins.Lust => new SinProfile { audioPitch = 1.15f, dialogueSpeed = 9f, tonePrefix = "<speed=9>" },
            E_CharacterSins.None => new SinProfile { audioPitch = 1.0f, dialogueSpeed = 10f, tonePrefix = "" },
            _ => new SinProfile { audioPitch = 1.0f, dialogueSpeed = 10f, tonePrefix = "" }
        };
    }
}
