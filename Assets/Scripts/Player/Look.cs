using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Bu class Camera rotation işlemleri ile sorumlu
/// </summary>
public class Look : MonoBehaviour
{
    
    [SerializeField] private  float _xSensivity = 1;
    [SerializeField] private float _ySensivity = 1;
    [SerializeField] private Transform _playerCamera;

    /// <summary>
    /// Cameranın max/min olabileceği açı.
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
        /// Input systemindeki ilgili tuşa basıldı mı kontrolü
        /// </summary>
        if(Keyboard.current.escapeKey.wasPressedThisFrame) 
            HideCursor(false);
        if(Mouse.current.leftButton.wasPressedThisFrame) 
            HideCursor(true);

        VerticalLookMovement();

        ///<summary>
        /// karakterin yatay dönmesinden sorulu code satırı.
        /// Kamerayı değil karakteri dödürüyor.
        /// </summary>
        transform.Rotate(transform.up,_lookInput.x*_xSensivity);
    }


    /// <summary>
    /// Kameranın Dikey açısınından sorumlu olan method.
    /// Sadece kamerayı çeviriyor. Kafa sabit kalıyor.
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
    /// Cusorun gizlenmesinden sorumlu method.
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
    /// _lookInput değerinin setlenmesinden sorumlu method.
    /// Mouse hareket ettiği zaman çalışıyor.
    /// </summary>
    /// <param name="pInput"></param>
    public void SetLookInput(Vector2 pInput)
    {
        _lookInput = pInput;
    }
}
