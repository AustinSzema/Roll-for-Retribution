using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Allows the player to switch between first and third person views.
/// </summary>
public class PlayerPOVSelector : MonoBehaviour
{
    /// <summary>
    /// Variable to store the player's current POV.
    /// </summary>
    [Tooltip("Variable to store the player's current POV.")]
    [SerializeField] private PlayerPOVVariable _currentPOV;
    
    /// <summary>
    /// The player's animator component.
    /// </summary>
    [Header("Component References")]
    [Tooltip("The player's animator component.")]
    [SerializeField] private Animator _playerAnimator;

    /// <summary>
    /// TransformReference for the first person camera.
    /// </summary>
    [Header("First Person View Components")]
    [Tooltip("TransformReference for the first person camera.")]
    [SerializeField] private TransformReferenceVariable _playerFirstPersonCameraTransform;

    /// <summary>
    /// Player character's rotation handler when in first person.
    /// </summary>
    [Tooltip("Player character's rotation handler when in first person.")]
    [SerializeField] private SyncYRotationToQuaternionVariable _playerRotationSync;

    /// <summary>
    /// TransformReference for the third person camera.
    /// </summary>
    [Header("Third Person View Components")]
    [Tooltip("TransformReference for the third person camera.")]
    [SerializeField] private TransformReferenceVariable _playerThirdPersonCameraTransform;

    /// <summary>
    /// Player character's rotation handler when in third person.
    /// </summary>
    [Tooltip("Player character's rotation handler when in thrid person.")]
    [SerializeField] private RotateBasedOnMainCameraRotation _playerRotateBased;

    /// <summary>
    /// TransformReference for the free fly camera.
    /// </summary>
    [Header("Free Fly View Components")]
    [Tooltip("TransformReference for the free fly camera.")]
    [SerializeField] private TransformReferenceVariable _playerFreeFlyCameraTransform;

    /// <summary>
    /// Player input component.
    /// </summary>
    [Tooltip("Player input component.")]
    [SerializeField] private PlayerInput _playerInput;

    /// <summary>
    /// Virtual camera component for the first person camera.
    /// </summary>
    private CinemachineVirtualCamera _playerFirstPersonCamera;

    /// <summary>
    /// Free look camera component for the third person camera.
    /// </summary>
    private CinemachineFreeLook _playerThirdPersonCamera;

    /// <summary>
    /// Virtual camera component for the free fly camera.
    /// </summary>
    private CinemachineVirtualCamera _playerFreeFlyCamera;

    /// <summary>
    /// The free fly camera the player can control.
    /// </summary>
    private FreeFlyCamera _freeFlyCamera;

    /// <summary>
    /// Parameter ID for the IsFirstPerson string.
    /// </summary>
    private int _isFirstPersonID;

    #region MonoBehaviour Methods
    private void Awake()
    {
        _isFirstPersonID = Animator.StringToHash("IsFirstPerson");
        _playerAnimator.SetBool(_isFirstPersonID, true);
    }
    private void Start()
    {
        _playerFirstPersonCamera =
            _playerFirstPersonCameraTransform.Value.GetComponent<CinemachineVirtualCamera>();
        _playerThirdPersonCamera =
            _playerThirdPersonCameraTransform.Value.GetComponent<CinemachineFreeLook>();
        _playerFreeFlyCamera =
            _playerFreeFlyCameraTransform.Value.GetComponent<CinemachineVirtualCamera>();
        _freeFlyCamera =
            _playerFreeFlyCameraTransform.Value.GetComponent<FreeFlyCamera>();

        SwitchToFirstPersonView();
    }
    #endregion

    /// <summary>
    /// Switches the player's perspective to first person view.
    /// </summary>
    public void SwitchToFirstPersonView()
    {
        _freeFlyCamera.ParentWhenInactive = _playerFirstPersonCameraTransform.Value;
        _freeFlyCamera.IsActive = false;
        _playerInput.SwitchCurrentActionMap("Gameplay");
        _playerAnimator.SetBool(_isFirstPersonID, true);
        _playerFirstPersonCamera.Priority = 1;
        _playerThirdPersonCamera.Priority = 0;
        _playerFreeFlyCamera.Priority = 0;
        _playerRotationSync.enabled = true;
        _playerRotateBased.enabled = false;

        _currentPOV.Value = PlayerPOV.FirstPerson;
    }

    /// <summary>
    /// Switches the player's perspective to first person view.
    /// </summary>
    public void SwitchToThirdPersonView()
    {
        _freeFlyCamera.ParentWhenInactive = _playerThirdPersonCameraTransform.Value;
        _freeFlyCamera.IsActive = false;
        _playerInput.SwitchCurrentActionMap("Gameplay");
        _playerAnimator.SetBool(_isFirstPersonID, false);
        _playerFirstPersonCamera.Priority = 0;
        _playerThirdPersonCamera.Priority = 1;
        _playerFreeFlyCamera.Priority = 0;
        _playerRotationSync.enabled = false;
        _playerRotateBased.enabled = true;

        _currentPOV.Value = PlayerPOV.ThirdPerson;
    }
    
    /// <summary>
    /// Switches the player's perspective to free fly view.
    /// </summary>
    public void SwitchToFreeFlyView()
    {
        _freeFlyCamera.IsActive = true;
        _playerInput.SwitchCurrentActionMap("FreeFlyCamera");
        _playerAnimator.SetBool(_isFirstPersonID, false);
        _playerFirstPersonCamera.Priority = 0;
        _playerThirdPersonCamera.Priority = 0;
        _playerFreeFlyCamera.Priority = 1;
        _playerRotationSync.enabled = false;
        _playerRotateBased.enabled = false;

        _currentPOV.Value = PlayerPOV.FreeFly;
    }
}
