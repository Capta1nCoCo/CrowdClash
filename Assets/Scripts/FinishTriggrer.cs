using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTriggrer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.WinAnimation();
        gameObject.SetActive(false);
    }
}
