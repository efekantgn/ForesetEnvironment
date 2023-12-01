using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement _movement;
    [SerializeField] Look _look;

    PlayerInputActionMap _inputs;
    PlayerInputActionMap.InGameControlActions _inGameControls;

    Vector2 _horizontalInput;
    Vector2 _mouseInput;
    bool _isSprinting ;

    // Start is called before the first frame update
    void Awake()
    {
        _inputs = new PlayerInputActionMap();
        _inGameControls = _inputs.InGameControl;

        _inGameControls.Movement.performed += C => _horizontalInput = C.ReadValue<Vector2>();
        _inGameControls.Look.performed += C => _mouseInput = C.ReadValue<Vector2>();
        _inGameControls.Sprint.started += C => _isSprinting = true;
        _inGameControls.Sprint.canceled += C => _isSprinting = false;
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    private void Update()
    {
        _movement.SetHorizontalInput(_horizontalInput,_isSprinting);
        _look.SetLookInput(_mouseInput);
    }
}
