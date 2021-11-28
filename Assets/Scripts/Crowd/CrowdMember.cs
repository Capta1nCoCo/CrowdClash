using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdMember : MonoBehaviour
{
    [SerializeField] private Material enemyMaterial;
    [SerializeField] private Material playerMaterial;

    private Renderer _renderer;
    private Crowd _crowd;
    
    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _crowd = GetComponentInParent<Crowd>();

        if (_crowd.MyCrowdType == Crowd.CrowdType.Enemy)
        {
            ChangeMaterialToEnemy();
        }
    }

    private void ChangeMaterialToEnemy()
    {
        _renderer.material = enemyMaterial;
    }
}
