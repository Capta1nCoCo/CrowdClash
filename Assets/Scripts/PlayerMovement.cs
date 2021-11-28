using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speedModifier = 2.5f;
    [SerializeField] private float gravity = -9.8f;
    
    private Touch _touch;
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        ApplyForwardMovement();
        ProcessHorizontalMovement();
    }

    private void ApplyForwardMovement()
    {
        _characterController.Move(Vector3.forward * speedModifier * Time.deltaTime);
    }
    
    private void ProcessHorizontalMovement()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {
                ProcessTranslation(_touch);
            }
        }
    }
    
    
    private void ProcessTranslation(Touch touch)
    {
        var deltaX = touch.deltaPosition.x * speedModifier;

        Vector3 movement = new Vector3(deltaX, 0, 0);
        movement = Vector3.ClampMagnitude(movement, speedModifier);

        movement.y = gravity;
        
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement);
    }

    
}
