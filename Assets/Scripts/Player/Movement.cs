using UnityEngine;

/// <summary>
/// The class responsible for the character's movement
/// Gravity is calculated manually here.
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
        /// A speed is created with incoming inputs.
        /// As a result, the character moves with speed.
        /// </summary>
        Vector3 Velocity = (transform.right*_horizontalInput.x+ transform.forward*_horizontalInput.y)*_speed;

        if (_isSprinting) 
            Velocity *= _sprintSpeedMultiplier;

        ResetVerticalVelocity();

        /// <summary>
        /// The calculated vertical velocity is added to the vector to transfer the vertical motion.
        /// </summary>
        Velocity.y = _verticalVelocity;
        _characterController.Move(Velocity * Time.deltaTime);

    }


    /// <summary>
    /// This method calcuşates the gravity of the character.
    /// It's falling with acceleration.
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
    /// Responsible for setting _horisontalInput value
    /// Runs when movement input invoked.
    /// </summary>
    /// <param name="pInput"></param>
    public void SetHorizontalInput(Vector2 pInput)
    {
        _horizontalInput = pInput;
    }


    /// <summary>
    /// Runs when jump input invoked.
    /// </summary>
    public void Jump()
    {
        if(_characterController.isGrounded)
            _verticalVelocity+=_jumpForce;
    }
    /// <summary>
    /// Responsible for setting _isSprinting value
    /// Runs when sprint input invoked.
    /// </summary>
    /// <param name="pInput"></param>
    public void SetSprinting(bool pIsSprinting)
    {
        _isSprinting = pIsSprinting;
    }
    
}
