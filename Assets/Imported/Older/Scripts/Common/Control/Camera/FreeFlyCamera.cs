using UnityEngine;

/// <summary>
/// Camera controlled directly for level exploration.
/// </summary>
public class FreeFlyCamera : MonoBehaviour
{
    /// <summary>
    /// Speed at which the camera moves.
    /// </summary>
    [Tooltip("Speed at which the camera moves.")]
    [SerializeField] private float _baseSpeed = 5.0f;

    /// <summary>
    /// Speed at which the camera moves when boosted.
    /// </summary>
    [Tooltip("Speed at which the camera moves when boosted.")]
    [SerializeField] private float _boostSpeed = 10.0f;

    /// <summary>
    /// Is the player currently controlling this camera?
    /// </summary>
    public bool IsActive
    {
        get
        {
            return _isActive;
        }
        set
        {
            _isActive = value;
            HandleParent();
        }
    }

    /// <summary>
    /// Is this camera currently moving at boost speed?
    /// </summary>
    public bool IsBoosted { get; set; } = false;

    /// <summary>
    /// Transform the camera is parented to when not being controlled.
    /// </summary>
    public Transform ParentWhenInactive { get; set; }

    /// <summary>
    /// Move input received from control class.
    /// </summary>
    public Vector2 MoveInput { get; set; }

    /// <summary>
    /// Vertical move input received from control class.
    /// </summary>
    public int VerticalInput { get; set; }

    /// <summary>
    /// Determines if the character is receiving input from a gamepad.
    /// </summary>
    public bool IsGamepadInput { get; set; }

    /// <summary>
    /// Movement vector of the camera along the xz plane.
    /// </summary>
    private Vector3 xzMovement;

    /// <summary>
    /// Is the player currently controlling this camera?
    /// </summary>
    private bool _isActive;

    /// <summary>
    /// CharacterController with which the camera will be controlled.
    /// </summary>
    [Tooltip("CharacterController with which the camera will be controlled.")]
    [SerializeField] private CharacterController _characterController;

    #region MonoBehaviour Methods
    private void Start()
    {
        HandleParent();
    }
    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        HandleXZMovement();
        _characterController.Move(xzMovement * Time.deltaTime);
    }
    #endregion

    /// <summary>
    /// Calculates the new movement speed of the character based on its walk 
    /// speed and acceleration parameters, updating the x and z components of 
    /// the final movement vector.
    /// </summary>
    private void HandleXZMovement()
    {
        float moveSpeed = IsBoosted ? _boostSpeed : _baseSpeed;
        if (IsGamepadInput)
        {
            xzMovement = ((transform.forward * MoveInput.y) +
                (transform.right * MoveInput.x)) * moveSpeed;
        }
        else
        {
            xzMovement = ((transform.forward * MoveInput.y) +
                (transform.right * MoveInput.x)).normalized *
                moveSpeed;
        }
    }

    /// <summary>
    /// Parents the camera to a designated transform when it is not being 
    /// controlled. Helps to keep the camera in an intuitive location when not
    /// in use.
    /// </summary>
    private void HandleParent()
    {
        if (ParentWhenInactive == null)
        {
            return;
        }

        if (IsActive)
        {
            transform.parent = null;
        }
        else
        {
            transform.parent = ParentWhenInactive;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
