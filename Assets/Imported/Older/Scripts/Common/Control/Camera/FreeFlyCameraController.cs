using UnityEngine;
using UnityEngine.InputSystem;

public class FreeFlyCameraController : MonoBehaviour
{
    /// <summary>
    /// TransformReference for the free fly camera.
    /// </summary>
    [Tooltip("TransformReference for the free fly camera.")]
    [SerializeField] private TransformReferenceVariable _freeFlyCameraTransformReference;

    /// <summary>
    /// Player input component.
    /// </summary>
    [Tooltip("Player input component.")]
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private PlayerPOVSelector _povSelector;

    /// <summary>
    /// Free fly camera to be controlled.
    /// </summary>
    private FreeFlyCamera _freeFlyCamera;

    #region MonoBehaviour Methods
    private void Start()
    {
        _freeFlyCamera =
            _freeFlyCameraTransformReference.Value.GetComponent<FreeFlyCamera>();
    }
    #endregion 

    #region Input Action Responses
    public void OnBoostInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _freeFlyCamera.IsBoosted = true;
        }
        else if (context.canceled)
        {
            _freeFlyCamera.IsBoosted = false;
        }
    }
    public void OnSwitchToFirstPersonInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _povSelector.SwitchToFirstPersonView();
        }
    }
    public void OnSwitchToThirdPersonInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _povSelector.SwitchToThirdPersonView();
        }
    }
    public void OnSwitchToFreeFlyInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _povSelector.SwitchToFreeFlyView();
        }
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _freeFlyCamera.IsGamepadInput =
            _playerInput.currentControlScheme == "Gamepad";

        if (context.performed)
        {
            _freeFlyCamera.MoveInput = context.ReadValue<Vector2>();
        }
        else
        {
            _freeFlyCamera.MoveInput = Vector2.zero;
        }
    }
    #endregion
}
