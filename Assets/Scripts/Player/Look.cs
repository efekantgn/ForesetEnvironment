using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for Look operations.
/// </summary>
public class Look : MonoBehaviour
{
    
    [SerializeField] private  float _xSensivity = 1;
    [SerializeField] private float _ySensivity = 1;
    [SerializeField] private Transform _playerCamera;

    /// <summary>
    /// The Angle that camera can be max/min.
    /// </summary>
    [SerializeField] private float _xClamp = 89f;
    private float _xRotation = 0f;
    private Vector2 _lookInput = Vector2.zero;

    private void Start()
    {
        HideCursor(true);
    }


    private void Update()
    {
        ///<summary>
        /// Input system has been pressed.
        /// </summary>
        if(Keyboard.current.escapeKey.wasPressedThisFrame) 
            HideCursor(false);
        if(Mouse.current.leftButton.wasPressedThisFrame) 
            HideCursor(true);

        VerticalLookMovement();

        ///<summary>
        /// The line of code responsible for horizontal rotation of the character.
        /// </summary>
        transform.Rotate(transform.up,_lookInput.x*_xSensivity);
    }


    /// <summary>
    /// The method responsible for the Vertical angle of the camera.
    /// </summary>
    private void VerticalLookMovement()
    {
        _xRotation -= _lookInput.y * _ySensivity;
        _xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);
        Vector3 TargetRot = transform.eulerAngles;
        TargetRot.x = _xRotation;
        _playerCamera.eulerAngles = TargetRot;
    }


    /// <summary>
    /// The method responsible for opening and closing the cursor.
    /// </summary>
    /// <param name="pValue">true for hide, false for show</param>
    private void HideCursor(bool pValue)
    {
        Cursor.visible = !pValue;

        if (pValue) 
            Cursor.lockState = CursorLockMode.Locked;
        else 
            Cursor.lockState = CursorLockMode.None;
    }


    /// <summary>
    /// Responsible for setting _lookInput value
    /// Runs when mouse moved.
    /// </summary>
    /// <param name="pInput"></param>
    public void SetLookInput(Vector2 pInput)
    {
        _lookInput = pInput;
    }
}
