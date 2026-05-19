using UnityEngine;

[CreateAssetMenu(fileName = "New Answer Options", menuName = "ScriptableObjects/Answer Options")]
public class AnswerOptionsSO : ScriptableObject
{
    [TextArea(4,4)] [Header("What Happened Before You Got Here")]
    public string Answer1Text;
    [TextArea(4,4)] [Header("What Kind of Life Did You Live")]
    public string Answer2Text;
    [TextArea(4,4)] [Header("What Was Your Job")]
    public string Answer3Text;
    [TextArea(4,4)] [Header("What Happened To Your Clothes")]
    public string Answer4Text;
    [TextArea(4,4)] [Header("Why Do You Have A Knife")]
    public string Answer5Text;

}
