using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.StopPlayerMovement(true);
        gameObject.SetActive(false);
        GameEvents.FlyAway(true);        
    }
}
