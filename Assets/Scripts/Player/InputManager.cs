using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Bu sınıf Input Action Assetten gelen inputların handle edilmesinden ve ilgili classlara gerekli bilgiyi iletmekten sorumlu.
/// </summary>
public class InputManager : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private Look _look;

    /// <summary>
    /// Input action assetin referansı. 
    /// Direk new ile initialize edilebiliyor çünkü C# scriptini generateledim.
    /// </summary>
    private PlayerInputActionMap _inputs;

    /// <summary>
    /// Input Action assetteki map erişimi.
    /// </summary>
    private PlayerInputActionMap.InGameControlActions _inGameControls;


    /// <summary>
    /// Asset ve Map Initialize edilmesi
    /// </summary>
    private void Awake()
    {
        _inputs = new PlayerInputActionMap();
        _inGameControls = _inputs.InGameControl;
       
    }

    /// <summary>
    /// İlgili actionlara methodların subscribe edilmesi.
    /// </summary>
    private void OnEnable()
    {
        _inputs.Enable();
        _inGameControls.Movement.performed += MovementPerformed;
        _inGameControls.Look.performed += LookPerformed;
        _inGameControls.Sprint.started += SprintStarted;
        _inGameControls.Sprint.canceled += SprintCanceled;
    }

    /// <summary>
    /// Subscribe edilen methodların unsubcribe edilmesi
    /// </summary>
    private void OnDisable()
    {
        _inGameControls.Movement.performed -= MovementPerformed;
        _inGameControls.Look.performed -= LookPerformed;
        _inGameControls.Sprint.started -= SprintStarted;
        _inGameControls.Sprint.canceled -= SprintCanceled;
        _inputs.Disable();
    }

    #region ActionMethods
    /// <summary>
    /// Sprint butonuna basıldığında Movement._isSprinting değerine true ataması yapılıyor.
    /// </summary>
    /// <param name="context">
    private void SprintStarted(InputAction.CallbackContext context)
    {
        _movement.SetSprinting(true);
    }
    /// <summary>
    /// Sprint butonuna basıldığında Movement._isSprinting değerine false ataması yapılıyor.
    /// </summary>
    /// <param name="context">
    private void SprintCanceled(InputAction.CallbackContext context)
    {
        _movement.SetSprinting(false);
    }

    /// <summary>
    /// Look actionı Invokelandığı zaman Look._lookInput değerine okunan değerin ataması yapılıyor.
    /// </summary>
    /// <param name="context">
    private void LookPerformed(InputAction.CallbackContext context)
    {
        _look.SetLookInput(context.ReadValue<Vector2>());
    }

    /// <summary>
    /// Movement actionı Invokelandığı zaman Movement._horizontalInput değerine okunan değerin ataması yapılıyor.
    /// </summary>
    /// <param name="context">
    private void MovementPerformed(InputAction.CallbackContext context)
    {
        _movement.SetHorizontalInput(context.ReadValue<Vector2>());
    }
    #endregion
}
