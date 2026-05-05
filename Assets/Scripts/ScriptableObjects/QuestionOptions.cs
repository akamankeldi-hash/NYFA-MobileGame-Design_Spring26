using UnityEngine;

[CreateAssetMenu(fileName = "New Question Options", menuName = "ScriptableObjects/Question Options")]
public class QuestionOptions : ScriptableObject
{
    [TextArea(4,4)]
    public string Question1Text;
    [TextArea(4,4)]
    public string Question2Text;
    [TextArea(4,4)]
    public string Question3Text;
    [TextArea(4,4)]
    public string Question4Text;
    [TextArea(4,4)]
    public string Question5Text;

}
