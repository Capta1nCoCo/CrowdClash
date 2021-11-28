using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrowdCounter : MonoBehaviour
{
    [SerializeField] private TextMeshPro crowdCounterText;

    private int _memberCounter;
    
    private void Awake()
    {
        UpdateMemberCounter(1);
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
