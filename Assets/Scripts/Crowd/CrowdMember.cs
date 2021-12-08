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
    private EffectManager _effectManager;

    private void Awake()
    {
        _crowd = GetComponentInParent<Crowd>();
        MyCrowdCounter = GetComponentInParent<CrowdCounter>();
        _effectManager = FindObjectOfType<EffectManager>();

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
        PlayDeathFX();
        CheckIfCrowdDies();
    }
    
    private void PlayDeathFX()
    {
        var index = _crowd.MyCrowdType == Crowd.CrowdType.Enemy ? 1 : 0;
        var rotation = index == 0 ? 180 : 0;
        
        GameObject deathFX = index == 0 ? _effectManager.GetGreenDeathFX() : _effectManager.GetRedDeathFX();
        deathFX.transform.position = transform.position;
        deathFX.transform.rotation = Quaternion.Euler(0f, rotation, 0f);

        GameObject splash = index == 0 ? _effectManager.GetGreenSplash() : _effectManager.GetRedSplash();
        splash.transform.position = new Vector3(transform.position.x, transform.position.y - 0.15f, transform.position.z);
        splash.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
    }

    private void CheckIfCrowdDies()
    {
        if (MyCrowdCounter.EqualsZero())
        {
            _crowd.KillCrowd();
        }
    }
    
}
