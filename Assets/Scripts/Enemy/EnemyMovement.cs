using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    
    private bool _isMoving;
    
    private CharacterController _characterController;
    private Transform _player;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        if (_isMoving)
        {
            _characterController.Move(Vector3.back * movementSpeed * Time.deltaTime);
        }
    }

    public void MoveTowardsPlayer()
    {
        transform.LookAt(_player);
        _isMoving = true;
    }
}
