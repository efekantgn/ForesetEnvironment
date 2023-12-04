using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for handling the inputs coming from the Input Action Asset 
/// and passing the necessary information to the relevant classes.
/// </summary>
public class InputManager : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private Look _look;

    /// <summary>
    /// Input action asset referance. 
    /// </summary>
    private PlayerInputActionMap _inputs;

    /// <summary>
    /// Input Action map referance.
    /// </summary>
    private PlayerInputActionMap.InGameControlActions _inGameControls;


    /// <summary>
    /// Asset and Map Initialization.
    /// </summary>
    private void Awake()
    {
        _inputs = new PlayerInputActionMap();
        _inGameControls = _inputs.InGameControl;
       
    }

    /// <summary>
    /// Subscribing methods to relevant actions.
    /// </summary>
    private void OnEnable()
    {
        _inputs.Enable();
        _inGameControls.Movement.performed += MovementPerformed;
        _inGameControls.Look.performed += LookPerformed;
        _inGameControls.Sprint.started += SprintStarted;
        _inGameControls.Sprint.canceled += SprintCanceled;
        _inGameControls.Jump.performed += JumpPerformed;
    }

    /// <summary>
    /// Unsubscribing subscribed methods
    /// </summary>
    private void OnDisable()
    {
        _inGameControls.Movement.performed -= MovementPerformed;
        _inGameControls.Look.performed -= LookPerformed;
        _inGameControls.Sprint.started -= SprintStarted;
        _inGameControls.Sprint.canceled -= SprintCanceled;
        _inGameControls.Jump.performed -= JumpPerformed;
        _inputs.Disable();
    }

    #region ActionMethods

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        _movement.Jump();
    }
    /// <summary>
    /// When the Sprint button is pressed, the Movement._isSprinting value is assigned to true.
    /// </summary>
    /// <param name="context">
    private void SprintStarted(InputAction.CallbackContext context)
    {
        _movement.SetSprinting(true);
    }
    /// <summary>
    /// When the Sprint button is pressed, the Movement._isSprinting value is assigned false .
    /// </summary>
    /// <param name="context">
    private void SprintCanceled(InputAction.CallbackContext context)
    {
        _movement.SetSprinting(false);
    }

    /// <summary>
    /// When the Look action is invoked, the read value is assigned to the Look._lookInput value.
    /// </summary>
    /// <param name="context">
    private void LookPerformed(InputAction.CallbackContext context)
    {
        _look.SetLookInput(context.ReadValue<Vector2>());
    }

    /// <summary>
    /// When the Movement action is invoked, the value read is assigned to the Movement._horizontalInput value.
    /// </summary>
    /// <param name="context">
    private void MovementPerformed(InputAction.CallbackContext context)
    {
        _movement.SetHorizontalInput(context.ReadValue<Vector2>());
    }
    #endregion
}
