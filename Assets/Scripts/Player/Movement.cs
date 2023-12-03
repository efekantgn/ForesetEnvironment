using UnityEngine;

/// <summary>
/// Karakterin hareketinden sorumlu olan class
/// Yerçekimi fiziğini class hesaplayıp işlem yapıyor.
/// </summary>
public class Movement : MonoBehaviour
{
    
    [SerializeField] private float _speed=3f;
    [SerializeField] private float _sprintSpeedMultiplier = 2f;
    [SerializeField] private float _jumpForce = 5f;
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
        /// <summary>
        /// Gelen inputlar ile bir hız oluşturuluyor.
        /// Bunun sonucunda da karakterin hareket etmesi hız ile yapılıyor.
        /// </summary>
        Vector3 Velocity = (transform.right*_horizontalInput.x+ transform.forward*_horizontalInput.y)*_speed;

        if (_isSprinting) 
            Velocity *= _sprintSpeedMultiplier;

        ResetVerticalVelocity();

        /// <summary>
        /// Dikey haraketin aktarılması için hesaplanan dikey hız vektöre ekleniyor.
        /// </summary>
        Velocity.y = _verticalVelocity;
        _characterController.Move(Velocity * Time.deltaTime);

    }


    /// <summary>
    /// Karakterin yerçekimini hesaplayan method.
    /// Düşerken hızını ivmeli olarak arttırılmasını sağlıyor.
    /// </summary>
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

    /// <summary>
    /// _horizontalInput değerinin setlenmesinden sorumlu method.
    /// Hareket inputu geldiği zaman çalışıyor.
    /// </summary>
    /// <param name="pInput"></param>
    public void SetHorizontalInput(Vector2 pInput)
    {
        _horizontalInput = pInput;
    }


    /// <summary>
    /// Jump tuşuna basıldığı zaman çalışıyor.
    /// </summary>
    public void Jump()
    {
        if(_characterController.isGrounded)
            _verticalVelocity+=_jumpForce;
    }
    /// <summary>
    /// _isSprinting değerinin setlenmesinden sorumlu method.
    /// Sprint tuşuna basıldığı zaman çalışıyor.
    /// </summary>
    /// <param name="pInput"></param>
    public void SetSprinting(bool pIsSprinting)
    {
        _isSprinting = pIsSprinting;
    }
    
}
