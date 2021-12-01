using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private bool _isMoving;
    
    private NavMeshAgent _navMeshAgent;
    private Transform _player;
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        if (_isMoving)
        {
            _navMeshAgent.SetDestination(_player.position);
        }
    }

    public void MoveTowardsPlayer()
    {
        transform.LookAt(_player);
        _isMoving = true;
    }
}
