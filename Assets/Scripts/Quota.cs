using NUnit.Framework;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Quota : MonoBehaviour
{
    [Header("Quota Setting")]
    public int requiredRightDecisions = 10;

    [Header("Current Stats")]
    private int currentRightDecision = 0;
    private int currentScore = 0;
    private bool isQuotaMet = false;

    public void AddRightDecision(int scorePoints)
    {
        if(isQuotaMet) return;
        currentRightDecision++;
        currentScore += scorePoints;
        Debug.Log($"Current Decision! Progress: {currentRightDecision}/{requiredRightDecisions} | Score: {currentScore}");
        CheckQuotaStatus();
    }

    public void AddWrongDecision(int scorePenalty)
    {
        if(isQuotaMet) return;
        currentScore = Mathf.Max(0, currentScore - scorePenalty);
        Debug.Log($"Wrong Decision! Score: {currentScore}");
    }
    private void CheckQuotaStatus()
    {
        if(currentRightDecision >= requiredRightDecisions)
        {
            isQuotaMet = true;
            WinGame();
        }
    }
    private void WinGame()
    {
        Debug.Log($"Quota met! Final Score: {currentScore}. Game is Completed!");
    }
}
