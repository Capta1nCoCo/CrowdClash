using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gateway : MonoBehaviour
{
    private enum OperationSign
    {
        Plus = 0,
        Multiply = 1,
        Minus = 2
    }
    
    [SerializeField] private GateKeeper gateKeeper;
    [SerializeField] private TextMeshPro gateText;
    [SerializeField] private OperationSign operationSign;
    [SerializeField] private int amount;
    [SerializeField] private Material[] materials;

    private char _sign;
    private int _amountAfterCalculation;
    
    private void Awake()
    {
        AdjustOperationSign();
        gateText.text = _sign.ToString() + amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        var crowdSize = other.gameObject.GetComponent<CrowdMember>().MyCrowdCounter.GetMemberCounter();
        AdjustAmount(crowdSize);
        
        gameObject.SetActive(false);
        if (gateKeeper != null)
        {
            gateKeeper.DeactivateGates();
        }
        GameEvents.ChangeCrowdSize(_amountAfterCalculation);
    }

    private void AdjustOperationSign()
    {
        switch (operationSign)
        {
            case OperationSign.Plus:
                _sign = '+';
                break;
            
            case OperationSign.Multiply:
                _sign = '*';
                break;
            
            case OperationSign.Minus:
                _sign = '-';
                GetComponent<MeshRenderer>().material = materials[1];
                break;
        }
    }

    private void AdjustAmount(int crowdSize)
    {
        switch (_sign)
        {
            case '+':
                _amountAfterCalculation = amount;
                break;
            
            case '*':
                _amountAfterCalculation = crowdSize * amount - crowdSize;
                break;
            
            case '-':
                _amountAfterCalculation = -amount;
                break;
        }
    }
}
