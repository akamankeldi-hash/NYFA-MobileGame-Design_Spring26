using System.Collections.Generic;
using UnityEngine;

public class PropRegistry : MonoBehaviour
{
    public static PropRegistry instance;

    private Dictionary<E_PropType, E_QuestionType> propToQuestion = new Dictionary<E_PropType, E_QuestionType>()
    { 
        {E_PropType.JobTool, E_QuestionType.WhatWasYourJob},
        {E_PropType.Wound, E_QuestionType.WhatHappenedBeforeYouGotHere}
    };

    void Awake() => instance = this;

    public E_QuestionType GetQuestionForProp(E_PropType prop) => propToQuestion[prop];
}
