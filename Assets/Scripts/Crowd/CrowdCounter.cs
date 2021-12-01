using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrowdCounter : MonoBehaviour
{
    [SerializeField] private TextMeshPro crowdCounterText;

    private int _memberCounter;
    public int GetMemberCounter() { return _memberCounter; }

    private void Awake()
    {
        UpdateCrowdCounterText();
    }

    public bool EqualsZero()
    {
        return _memberCounter <= 0;
    }
    
    public void UpdateMemberCounter(int amount)
    {
        _memberCounter += amount;
        UpdateCrowdCounterText();
    }

    private void UpdateCrowdCounterText()
    {
        crowdCounterText.text = _memberCounter.ToString();
    }
}
