using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    
    [SerializeField] private float _speed=3f;
    [SerializeField] private float _sprintSpeedMultiplier = 2f;
    [SerializeField] private float _gravity = -9.81f;
    
    private bool _isSprinting = false;
    private float _verticalVelocity = 0;
    private CharacterController _characterController;
    private Vector2 _horizontalInput;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {

        Vector3 Velocity = _speed*(transform.right*_horizontalInput.x+ transform.forward*_horizontalInput.y);

        if (_isSprinting) 
            Velocity *= _sprintSpeedMultiplier;

        ResetVerticalVelocity();
        Velocity.y = _verticalVelocity;
        _characterController.Move(Velocity * Time.deltaTime);

    }

    private void ResetVerticalVelocity()
    {
        if (_verticalVelocity < 0.0f && _characterController.isGrounded)
        {
            _verticalVelocity = -1f;
        }
        else
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }
    }

    public void SetHorizontalInput(Vector2 pInput)
    {
        _horizontalInput = pInput;
    }
    public void SetSprinting(bool pIsSprinting)
    {
        _isSprinting = pIsSprinting;
    }
    
}
