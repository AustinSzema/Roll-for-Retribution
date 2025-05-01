using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [Header("Component References")]
    public CinemachineInputProvider CinemachineInputProvider;
    [SerializeField] private Mover _playerMover;
    [SerializeField] private Interactor _playerInteractor;

    [Header("Optional References")]
    [SerializeField] private HandDrum _handDrum;
    [SerializeField] private Animator _letterAndOpenerAnimator;
    [SerializeField] private Animator _candleLighterAnimator;

    public bool CanMove { get; set; } = true;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnActivateInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_handDrum != null && _handDrum.gameObject.activeInHierarchy)
            {
                _handDrum.Strike();
            }
            if (_letterAndOpenerAnimator != null && _letterAndOpenerAnimator.gameObject.activeInHierarchy)
            {
                _letterAndOpenerAnimator.SetTrigger("Open");
            }
            if (_candleLighterAnimator != null && _candleLighterAnimator.gameObject.activeInHierarchy)
            {
                _candleLighterAnimator.SetTrigger("Light");
            }
            _playerInteractor.Activate();
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed && CanMove)
        {
            _playerMover.MoveInput = context.ReadValue<Vector2>();
        }
        else
        {
            _playerMover.MoveInput = Vector2.zero;
        }
    }
}
