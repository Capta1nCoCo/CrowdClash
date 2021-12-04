using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdMember : MonoBehaviour
{
    [SerializeField] private Material enemyMaterial;
    [SerializeField] private Material playerMaterial;
    [SerializeField] private SkinnedMeshRenderer body;    
    
    public bool EnemyCrowdMember { get; private set; }
    public CrowdCounter MyCrowdCounter { get; private set; }
    
    private Crowd _crowd;


    private void Awake()
    {
        _crowd = GetComponentInParent<Crowd>();
        MyCrowdCounter = GetComponentInParent<CrowdCounter>();

        if (_crowd != null)
        {          
            if (_crowd.MyCrowdType == Crowd.CrowdType.Enemy)
            {
                ChangeMaterialToEnemy();
                EnemyCrowdMember = true;
            }
        }       
    }
    
    private void ChangeMaterialToEnemy()
    {
        body.material = enemyMaterial;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var collidedObject = other.gameObject;
        if (collidedObject.GetComponent<CrowdMember>() || collidedObject.GetComponent<Trap>())
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        MyCrowdCounter.UpdateMemberCounter(-1);
        CheckIfCrowdDies();
    }
    
    private void CheckIfCrowdDies()
    {
        if (MyCrowdCounter.EqualsZero())
        {
            _crowd.KillCrowd();
        }
    }
    
}
