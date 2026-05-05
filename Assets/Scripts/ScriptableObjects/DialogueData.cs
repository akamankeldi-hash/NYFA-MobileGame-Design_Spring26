using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "ScriptableObjects/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [TextArea(4,4)]
    public List<string> conversationBlock;
    [TextArea(4,4)]
    public string whatHappened;
    [TextArea(4,4)]
    public string howDidHeLive;
    [TextArea(4,4)]
    public string whyDoYouHaveA;
    [TextArea(4,4)]
    public string whatsWithClothes;
    [TextArea(4,4)]
    public string howDidYouGetHurt;
}