using UnityEngine;
using UnityEngine.EventSystems;

public class PropItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private E_PropType propType;
    [SerializeField] private GameObject glowIndicator;
    private bool hasBeenInspected = false;

    void Start()
    {
        if(glowIndicator) glowIndicator.SetActive(true);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (hasBeenInspected) return;
        hasBeenInspected = true;

        if (glowIndicator) glowIndicator.SetActive(false);

        // E_QuestionType unlockedQuestion 
    }

    public void ResetProp()
    {
        
    }
}
