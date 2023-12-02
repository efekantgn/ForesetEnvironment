using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    [SerializeField] private  float _xSensivity = 1;
    [SerializeField] private float _ySensivity = 1;

    [SerializeField] private Transform _playerCamera;
    [SerializeField] private float _xClamp=89f;
    private float _xRotation =0f;

    private Vector2 _lookInput = Vector2.zero;

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
        if(Keyboard.current.escapeKey.wasPressedThisFrame) HideCursor(false);
        if(Mouse.current.leftButton.wasPressedThisFrame) HideCursor(true);

        transform.Rotate(transform.up,_lookInput.x*_xSensivity);
        VerticalLookMovement();
    }

    private void VerticalLookMovement()
    {
        _xRotation -= _lookInput.y * _ySensivity;
        _xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);
        Vector3 TargetRot = transform.eulerAngles;
        TargetRot.x = _xRotation;
        _playerCamera.eulerAngles = TargetRot;
    }

    private void HideCursor(bool pValue)
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
