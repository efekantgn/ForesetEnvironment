using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void Start()
    {
        _inGameControls.Movement.performed += C => _horizontalInput = C.ReadValue<Vector2>();
        _inGameControls.Look.performed += C => _mouseInput = C.ReadValue<Vector2>();
        _inGameControls.Sprint.started += C => _isSprinting = true;
        _inGameControls.Sprint.canceled += C => _isSprinting = false;
    }


    private void Update()
    {
        _movement.SetHorizontalInput(_horizontalInput,_isSprinting);
        _look.SetLookInput(_mouseInput);
    }
    private void OnDisable()
    {
        _inputs.Disable();
    }
}
