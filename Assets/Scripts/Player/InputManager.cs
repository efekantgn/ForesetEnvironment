using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private Look _look;
    private PlayerInputActionMap _inputs;
    private PlayerInputActionMap.InGameControlActions _inGameControls;
    private Vector2 _horizontalInput;
    private Vector2 _mouseInput;
    private bool _isSprinting ;

    
    private void Awake()
    {
        _inputs = new PlayerInputActionMap();
        _inGameControls = _inputs.InGameControl;
       
    }
    private void OnEnable()
    {
        _inputs.Enable();
        _inGameControls.Movement.performed += MovementPerformed;
        _inGameControls.Look.performed += LookPerformed;
        _inGameControls.Sprint.started += SprintStarted;
        _inGameControls.Sprint.canceled += SprintCanceled;
    }
    private void OnDisable()
    {
        _inGameControls.Movement.performed -= MovementPerformed;
        _inGameControls.Look.performed -= LookPerformed;
        _inGameControls.Sprint.started -= SprintStarted;
        _inGameControls.Sprint.canceled -= SprintCanceled;
        _inputs.Disable();
    }

    
    private void SprintStarted(InputAction.CallbackContext context)
    {
        _isSprinting = true;
        _movement.SetSprinting(_isSprinting);
    }
    private void SprintCanceled(InputAction.CallbackContext context)
    {
        _isSprinting = false;
        _movement.SetSprinting(_isSprinting);
    }

    private void LookPerformed(InputAction.CallbackContext context)
    {
        _mouseInput = context.ReadValue<Vector2>();
        _look.SetLookInput(_mouseInput);
    }

    private void MovementPerformed(InputAction.CallbackContext context)
    {
        _horizontalInput = context.ReadValue<Vector2>();
        _movement.SetHorizontalInput(_horizontalInput);
    }

}
