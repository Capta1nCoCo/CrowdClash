using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedModifier = 2.5f;
    [SerializeField] private float horizontalSpeed = 1f;
    [SerializeField] private float gravity = 5f;

    private const float _xBound = 0.917f;
    private const float _widthPerUnit = 0.07f;
    private float _xPadding;

    private bool _stoppedMoving;
    private bool _isUncontrollable;
    private bool _isFlyingAway;
    private bool _isClamped;
    private float _groundedPos;

    private Touch _touch;
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _stoppedMoving = false;
        _groundedPos = transform.position.y;
        _xPadding = _xBound;

        GameEvents.StopPlayerMovement += StopPlayerMovement;
        GameEvents.FlyAway += FlyAway;
        GameEvents.AdjustPlayerWidth += AdjustPlayerWidth;
    }
    
    private void OnDestroy()
    {
        GameEvents.StopPlayerMovement -= StopPlayerMovement;
        GameEvents.FlyAway -= FlyAway;
        GameEvents.AdjustPlayerWidth -= AdjustPlayerWidth;
    }

    private void FixedUpdate()
    {
        if (!_stoppedMoving)
        {
            ApplyForwardMovement();            
            if (!_isUncontrollable)
            {
                ProcessHorizontalMovement();                
            }
            
            if (!_isFlyingAway && transform.position.y > _groundedPos)
            {
                ApplyGravity();
            }
        }

        if (_isFlyingAway)
        {
            ApplyUpwardMovement();
        }        
    }
    
    private void StopPlayerMovement(bool confirm)
    {
        _stoppedMoving = confirm;
        ResetHorizontalPosition();
    }

    public void StopPlayersControl()
    {
        _isUncontrollable = true;
        DoubleSpeedModifier();
    }

    private void DoubleSpeedModifier()
    {
        speedModifier += speedModifier;
    }

    private void FlyAway(bool state)
    {
        _isFlyingAway = state;
    }

    private void ResetHorizontalPosition()
    {
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    private void ApplyForwardMovement()
    {
        _characterController.Move(Vector3.forward * speedModifier * Time.deltaTime);
    }

    private void ApplyUpwardMovement()
    {
        _characterController.Move(Vector3.up * speedModifier * Time.deltaTime);
    }
    
    private void ApplyGravity()
    {
        _characterController.Move(Vector3.down * gravity * Time.deltaTime);
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
        var deltaX = touch.deltaPosition.x * horizontalSpeed;

        Vector3 movement = new Vector3(deltaX, 0, 0);
        movement = Vector3.ClampMagnitude(movement, horizontalSpeed);

        //movement.y = gravity;
        ClampCrowdMovement(touch);

        if (!_isClamped)
        {
            movement *= Time.deltaTime;
        }
        else
        {
            movement *= 0;
        }
        //movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement);
    }

    private void ClampCrowdMovement(Touch touch)
    {
        if (transform.position.x > 0)
        {
            ClampRight(touch);
        }
        else
        {
            ClampLeft(touch);
        }
    }

    private void ClampRight(Touch touch)
    {
        if (transform.position.x > _xPadding && touch.deltaPosition.x > 0)
        {
            _isClamped = true;
        }
        else if (touch.deltaPosition.x < 0)
        {
            _isClamped = false;
        }
    }


    private void ClampLeft(Touch touch)
    {
        if (transform.position.x < -_xPadding && touch.deltaPosition.x < 0)
        {
            _isClamped = true;
        }
        else if (touch.deltaPosition.x > 0)
        {
            _isClamped = false;
        }
    }

    private void AdjustPlayerWidth(int units)
    {        
        if (units == 0)
        {
            _xPadding = _xBound;
        }
        else
        {
            _xPadding -= _widthPerUnit * units;
        }        
    }
}
