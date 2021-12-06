using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyToControl;
    [SerializeField] private float delayExtermination = 2f;

    private Crowd _crowd;
    private CrowdCounter _crowdCounter;

    private void Awake()
    {
        _crowd = enemyToControl.gameObject.GetComponent<Crowd>();
        _crowdCounter = enemyToControl.gameObject.GetComponent<CrowdCounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var collidedObject = other.gameObject;
        if (collidedObject.GetComponent<CrowdMember>() != null)
        {
            if (!collidedObject.GetComponent<CrowdMember>().EnemyCrowdMember)
            {
                GameEvents.StopPlayerMovement(true);
                enemyToControl.MoveTowardsPlayer();
                StartCoroutine(IncludePossibleLeftovers());
            }
        }
    }

    public void RemoveEnemyZone()
    {
        gameObject.SetActive(false);
        GameEvents.StopPlayerMovement(false);
    }

    private IEnumerator IncludePossibleLeftovers()
    {
        yield return new WaitForSeconds(delayExtermination);
        
        var leftovers = _crowdCounter.GetMemberCounter();
        GameEvents.ChangeCrowdSize(-leftovers);
        _crowd.KillCrowd();        
    }
}
