using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    
    [SerializeField] float _speed=3f;
    [SerializeField] float _sprintSpeedMultiplier = 2f;
    bool _isSprinting = false;

    [SerializeField] float _gravity = -9.81f;
    Vector3 _verticalVelocity = Vector3.zero;


    CharacterController _characterController;
    Vector2 _horizontalInput;

    [Header("Ground Check")]
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundCheckRadius;
    [SerializeField] LayerMask _groundCheckLayerMask;
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(IsPlayerGrounded()) _verticalVelocity.y = 0f;

        Vector3 HorizontalVelocity = _speed*(transform.right*_horizontalInput.x+ transform.forward*_horizontalInput.y);

        if (_isSprinting) HorizontalVelocity *= _sprintSpeedMultiplier;

        _characterController.Move(HorizontalVelocity*Time.deltaTime);

        _verticalVelocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_verticalVelocity * Time.deltaTime);

    }

    public bool IsPlayerGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundCheckLayerMask);
    }

    public void SetHorizontalInput(Vector2 pInput,bool pIsSprinting)
    {
        _horizontalInput = pInput;
        _isSprinting = pIsSprinting;
    }
    
}
