using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gateway : MonoBehaviour
{
    [SerializeField] private GateKeeper gateKeeper;
    [SerializeField] private TextMeshPro gateText;
    [SerializeField] private int amount;
    
    private void Awake()
    {
        gateText.text = "+" + amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        if (gateKeeper != null)
        {
            gateKeeper.DeactivateGates();
        }
        GameEvents.ExpandCrowd(amount);
    }
    
}
