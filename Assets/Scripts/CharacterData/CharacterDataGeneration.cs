using UnityEngine;

public class CharacterDataGeneration : MonoBehaviour
{
    public static CharacterDataGeneration instance;
    [SerializeField] private CharacterData characterData;
    [SerializeField] private CharacterTemplateSO[] templatePool;
    [SerializeField] private PropItem[] propItems;
    private CharacterTemplateSO currentTemplate;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GenerateCharacter();
    }

    private void GenerateCharacter()
    {
        if (templatePool != null || templatePool.Length == 0)
        {
            Debug.LogError("Template Pool is empty");
            return;
        }

        currentTemplate = templatePool[Random.Range(0, templatePool.Length)];
        ApplyTemplate(currentTemplate);
    }

    public CharacterTemplateSO GetCurrentTemplate() => currentTemplate;

    private void ApplyTemplate(CharacterTemplateSO template)
    {
        characterData.SetData(template);
        
        ResetAllProps();
        ActivateProps(template.activeProps);
    }

    private void ResetAllProps()
    {
        foreach (var prop in propItems)
        {
            if(prop != null)
            {
                prop.gameObject.SetActive(false);
                prop.ResetProp();
            }
        }
    }

    private void ActivateProps(E_PropType[] activeProps)
    {
        if(activeProps == null) return;
        
        foreach(var propType in activeProps)
        {
            int index = (int)propType;
            if(index < propItems.Length && propItems[index] != null)
            {
                propItems[index].gameObject.SetActive(true);
            }
        }
    }

    public static E_CharacterSins RollSin()
    {
        if (Random.value > 0.20f) return E_CharacterSins.None;
        return (E_CharacterSins)Random.Range(1,8);
    }
}
