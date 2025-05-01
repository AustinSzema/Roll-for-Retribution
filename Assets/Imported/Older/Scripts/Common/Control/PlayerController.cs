using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Updates the player state machine based on user input.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// State machine driving the player's state context logic.
    /// </summary>
    [Tooltip("State machine driving the player's state context logic.")]
    [SerializeField] private PlayerStateMachine _playerStateMachine;

    [SerializeField] private RotateBasedOnMainCameraRotation _rotator;

    [SerializeField] private PlayerInteractor _playerInteractor;

    [SerializeField] private PlayerPOVSelector _povSelector;

    /// <summary>
    /// Player input component.
    /// </summary>
    [Tooltip("Player input component.")]
    [SerializeField] private PlayerInput _playerInput;

    #region Input Action Responses
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
    public void OnCrouchInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerStateMachine.IsCrouchPressed = !_playerStateMachine.IsCrouchPressed;
        }
    }
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerInteractor.Interact();
        }
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        _playerStateMachine.IsJumpPressed = context.ReadValueAsButton();
        _playerStateMachine.RequireNewJumpPress = false;
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _playerStateMachine.IsGamepadInput = _playerInput.currentControlScheme == "Gamepad";

        if (context.performed)
        {
            _playerStateMachine.MoveInput = context.ReadValue<Vector2>();
            _rotator.MoveInput = context.ReadValue<Vector2>();
        }
        else
        {
            _playerStateMachine.MoveInput = Vector2.zero;
            _rotator.MoveInput = Vector2.zero;
        }
    }
    public void OnSprintInput(InputAction.CallbackContext context)
    {
        if (context.started || context.canceled)
        {
            _playerStateMachine.IsSprintPressed = !_playerStateMachine.IsSprintPressed;
        }
    }
    public void OnToggleWalkRunInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerStateMachine.IsRunPressed = !_playerStateMachine.IsRunPressed;
        }
    }
    public void OnWalkRunInput(InputAction.CallbackContext context)
    {
        if (context.started || context.canceled)
        {
            _playerStateMachine.IsRunPressed = !_playerStateMachine.IsRunPressed;
        }
    }
    #endregion
}
