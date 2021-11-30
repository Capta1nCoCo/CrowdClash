using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyToControl;
    
    private void OnTriggerEnter(Collider other)
    {
        var collidedObject = other.gameObject;
        if (collidedObject.GetComponent<CrowdMember>() != null)
        {
            if (!collidedObject.GetComponent<CrowdMember>().EnemyCrowdMember)
            {
                GameEvents.StopPlayerMovement(true);
                enemyToControl.MoveTowardsPlayer();
            }
        }
    }

    public void TellThatCrowdIsDead()
    {
        gameObject.SetActive(false);
        GameEvents.StopPlayerMovement(false);
    }
}
