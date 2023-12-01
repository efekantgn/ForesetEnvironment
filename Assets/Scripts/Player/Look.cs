using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    [SerializeField] float _xSensivity = 1;
    [SerializeField] float _ySensivity = 1;

    [SerializeField] Transform _playerCamera;
    [SerializeField] float _xClamp=89f;
    float _xRotation=0f;
    
    Vector2 _lookInput = Vector2.zero;

    private void Start()
    {
        HideCursor(true);
    }

    public void SetLookInput(Vector2 pInput)
    {
        _lookInput = pInput;
    }

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame) HideCursor(false);
        if(Mouse.current.rightButton.wasPressedThisFrame) HideCursor(true);
        transform.Rotate(transform.up,_lookInput.x*_xSensivity);
        VerticalLookMovement();
    }

    void VerticalLookMovement()
    {
        _xRotation -= _lookInput.y * _ySensivity;
        _xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);
        Vector3 TargetRot = transform.eulerAngles;
        TargetRot.x = _xRotation;
        _playerCamera.eulerAngles = TargetRot;
    }

    public void HideCursor(bool pValue)
    {
        if (pValue)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
