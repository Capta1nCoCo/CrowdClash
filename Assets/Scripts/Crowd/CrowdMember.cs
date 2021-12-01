using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdMember : MonoBehaviour
{
    [SerializeField] private Material enemyMaterial;
    [SerializeField] private Material playerMaterial;

    public bool EnemyCrowdMember { get; private set; }
    
    private Renderer _renderer;
    private Crowd _crowd;
    public CrowdCounter MyCrowdCounter { get; private set; }

    
    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _crowd = GetComponentInParent<Crowd>();
        MyCrowdCounter = GetComponentInParent<CrowdCounter>();

        if (_crowd.MyCrowdType == Crowd.CrowdType.Enemy)
        {
            ChangeMaterialToEnemy();
            EnemyCrowdMember = true;
        }
    }
    
    private void ChangeMaterialToEnemy()
    {
        _renderer.material = enemyMaterial;
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
