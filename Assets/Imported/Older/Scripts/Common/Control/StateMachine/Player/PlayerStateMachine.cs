using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Context of the state machine. Stores the persistent state data that is
/// passed to the active concrete state(s). This data is used for their logic
/// and switching between states.
/// </summary>
public class PlayerStateMachine : MonoBehaviour
{
    /// <summary>
    /// Animator for this character.
    /// </summary>
    [Header("Component References")]
    [Tooltip("Animator for this character.")]
    public Animator Animator;

    /// <summary>
    /// CharacterController responsible for moving the character.
    /// </summary>
    [Tooltip("CharacterController responsible for moving the character.")]
    public CharacterController CharacterController;

    /// <summary>
    /// Sensor to check if the character is grounded.
    /// </summary>
    [Tooltip("Sensor to check if the character is grounded.")]
    public Sensor3D GroundSensor;

    /// <summary>
    /// String variable to store the current hierarchical state name.
    /// </summary>
    [Tooltip("String variable to store the current hierarchical state name.")]
    public StringVariable CurrentStateStringVariable;

    /// <summary>
    /// Sensor to check if the character has room to uncrouch.
    /// </summary>
    [Tooltip("Sensor to check if the character has room to uncrouch.")]
    public Sensor3D HeadSensor;

    /// <summary>
    /// Component used in third person view to rotate the player transform.
    /// </summary>
    [Tooltip("Component used in third person view to rotate the player " + 
        "transform.")]
    [SerializeField] private RotateBasedOnMainCameraRotation _rotateBasedOnMainCameraRotation;

    /// <summary>
    /// Center of the characterController when crouched.
    /// </summary>
    [Header("Crouching")]
    [Tooltip("Center of the characterController when crouched.")]
    public Vector3 CrouchCenter = new Vector3(0.05f, 0.66f, 0.05f);

    /// <summary>
    /// Height of the characterController when crouched.
    /// </summary>
    [Tooltip("Height of the characterController when crouched.")]
    public float CrouchHeight = 1.32f;

    /// <summary>
    /// Radius of the characterController when crouched.
    /// </summary>
    [Tooltip("Radius of the characterController when crouched.")]
    public float CrouchRadius = 0.44f;

    /// <summary>
    /// Scale the speed by this value when crouched.
    /// </summary>
    [Tooltip("Scale the speed by this value when crouched.")]
    public float CrouchSpeedMultiplier = 0.5f;

    /// <summary>
    /// How long it takes for the character to crouch.
    /// </summary>
    [Tooltip("How long it takes for the character to crouch.")]
    public float CrouchTime = 0.25f;

    /// <summary>
    /// Scale of the gravity acting on this character.
    /// </summary>
    [Header("Jumping & Gravity")]
    [Tooltip("Scale of gravity acting on this character.")]
    [SerializeField] private float _gravityScale = 1.0f;

    /// <summary>
    /// How long after the jump to begin checking for groundedness.
    /// </summary>
    [Tooltip("How long after the jump to begin checking for groundedness.")]
    public float JumpDisableGroundSensorTime = 0.1f;

    /// <summary>
    /// How high the character can jump.
    /// </summary>
    [Tooltip("How high the character can jump.")]
    public float JumpHeight = 3.2f;

    /// <summary>
    /// Maximum velocity at which this character can fall.
    /// </summary>
    [Tooltip("Maximum velocity at which this character can fall.")]
    public float MaxFallVelocity = 50.0f;

    /// <summary>
    /// Rate at which the character approaches its target speed. Set to -1 for
    /// instant speed change.
    /// </summary>
    [Header("Locomotion")]
    [Tooltip("Rate at which the character approaches its target speed. Set " +
        "to -1.0f for instant speed change.")]
    public float Acceleration = -1.0f;

    /// <summary>
    /// Scale the speed of the player by this value when standing.
    /// </summary>
    [Tooltip("Scale the speed of the player by this value when standing.")]
    public float BaseSpeedMultipler = 1.0f;

    /// <summary>
    /// Run speed of the character.
    /// </summary>
    [Tooltip("Run speed of the character.")]
    [SerializeField] private float _runSpeed = 5.0f;

    /// <summary>
    /// Scale the speed by this value when sprinting.
    /// </summary>
    [Tooltip("Scale the speed by this value when sprinting.")]
    public float SprintSpeedMultiplier = 1.5f;

    /// <summary>
    /// Walk speed of the character.
    /// </summary>
    [Tooltip("Walk speed of the character.")]
    [SerializeField] private float _walkSpeed = 1.5f;

    /// <summary>
    /// The final movement vector of the character each frame.
    /// </summary>
    [HideInInspector]
    public Vector3 Movement;

    /// <summary>
    /// PlayerStateFactory to generate new concrete states for the state 
    /// machine.
    /// </summary>
    private PlayerStateFactory _states;

    /// <summary>
    /// Couroutine currently active on this MonoBehaviour
    /// </summary>
    public Coroutine ActiveRoutine = null;

    /// <summary>
    /// Can this character sprint in its current state?
    /// </summary>
    public bool CanSprint { get; set; } = true;

    /// <summary>
    /// Active state of the state machine.
    /// </summary>
    public PlayerBaseState CurrentState { get; set; }

    /// <summary>
    /// Center of the characterController when standing.
    /// </summary>
    public Vector3 StandCenter { get; private set; }

    /// <summary>
    /// Height of the characterController when standing.
    /// </summary>
    public float StandHeight { get; private set; }

    /// <summary>
    /// Radius of the characterController when standing.
    /// </summary>
    public float StandRadius { get; private set; }

    /// <summary>
    /// Scaled gravity vector.
    /// </summary>
    public float Gravity { get; private set; }

    /// <summary>
    /// Determines if the crouch input is pressed.
    /// </summary>
    public bool IsCrouchPressed { get; set; } = false;

    /// <summary>
    /// Parameter ID for the IsCrouched string.
    /// </summary>
    public int IsCrouchedID { get; private set; }

    /// <summary>
    /// Determines if the character is receiving input from a gamepad.
    /// </summary>
    public bool IsGamepadInput { get; set; } = false;

    /// <summary>
    /// Parameter ID for the IsGrounded string.
    /// </summary>
    public int IsGroundedID { get; private set; }

    /// <summary>
    /// Determines if the jump input has been pressed.
    /// </summary>
    public bool IsJumpPressed { get; set; } = false;

    /// <summary>
    /// Determines if the run input is pressed.
    /// </summary>
    public bool IsRunPressed { get; set; } = false;

    /// <summary>
    /// Determines if the character is currently running.
    /// </summary>
    public bool IsRunning
    {
        get
        {
            return (IsRunPressed || IsGamepadInput);
        }
    }

    /// <summary>
    /// Determines if the character is currently sprinting.
    /// </summary>
    public bool IsSprinting
    {
        get
        {
            bool isMovingForward = MoveInput.y >= 0;
            return (IsSprintPressed && CanSprint && isMovingForward);
        }
    }

    /// <summary>
    /// Determines if the sprint input is pressed.
    /// </summary>
    public bool IsSprintPressed { get; set; } = false;

    /// <summary>
    /// Parameter ID for the JumpTrigger string.
    /// </summary>
    public int JumpTriggerID { get; private set; }

    /// <summary>
    /// Move input received from control class.
    /// </summary>
    public Vector2 MoveInput { get; set; }

    /// <summary>
    /// Variable to store intermediate speed calculation during acceleration.
    /// </summary>
    public float NewSpeed { get; set; } = 0;

    /// <summary>
    /// Determines whether the player requires a new press of the jump input
    /// to jump again.
    /// </summary>
    public bool RequireNewJumpPress { get; set; }

    /// <summary>
    /// Run speed of the character.
    /// </summary>
    public float RunSpeed
    {
        get
        {
            return _runSpeed * CurrentSpeedMultiplier;
        }
    }

    /// <summary>
    /// Speed scalar to be changed based on state.
    /// </summary>
    public float CurrentSpeedMultiplier { get; set; }

    /// <summary>
    /// List that stores which substates are currently active.
    /// </summary>
    public List<PlayerBaseState> SubstateList { get; set; } =
        new List<PlayerBaseState>();

    /// <summary>
    /// Parameter ID for the VelocityX string.
    /// </summary>
    private int _speedID;

    /// <summary>
    /// Parameter ID for the VelocityX string.
    /// </summary>
    private int _velocityXID;

    /// <summary>
    /// Parameter ID for the VelocityZ string.
    /// </summary>
    private int _velocityZID;

    /// <summary>
    /// Walk speed of the character.
    /// </summary>
    public float WalkSpeed
    {
        get
        {
            return _walkSpeed * CurrentSpeedMultiplier;
        }
    }

    #region MonoBehaviour Methods
    private void Awake()
    {
        Gravity = Physics.gravity.y * _gravityScale;

        StandCenter = CharacterController.center;
        StandRadius = CharacterController.radius;
        StandHeight = CharacterController.height;

        AssignAnimatorIDs();

        _states = new PlayerStateFactory(this);
        CurrentState = _states.Grounded();
        CurrentState.EnterStates();
    }
    private void Start()
    {
        UpdateCurrentStateString(CurrentState);
    }
    private void Update()
    {
        CurrentState.UpdateStates();
        HandleGravity();
        ApplyMovement();
    }
    #endregion

    /// <summary>
    /// Returns the move input scaled by the magnitude of the character
    /// controller's current velocity. Useful for updating the Animator.
    /// </summary>
    /// <returns>Vector2 representing the move input scaled by the magnitude of 
    /// the character controller's velocity.</returns>
    public Vector2 GetScaledMoveInput()
    {
        if (MoveInput == Vector2.zero)
        {
            return Vector2.zero;
        }
        else
        {
            return MoveInput * CharacterController.velocity.magnitude;
        }
    }

    /// <summary>
    /// Applies the final movement vector to the character controller.
    /// </summary>
    private void ApplyMovement()
    {
        if (_rotateBasedOnMainCameraRotation.enabled)
        {
            Vector3 horizontalMovement = _rotateBasedOnMainCameraRotation.TargetDirection.normalized *
                (NewSpeed * Time.deltaTime);
            Vector3 verticalMovement = new Vector3(0.0f, Movement.y, 0.0f) *
                Time.deltaTime;
            CharacterController.Move(horizontalMovement + verticalMovement);
        }
        else
        {
            CharacterController.Move(Movement * Time.deltaTime);
        }
        Animator.SetFloat(_speedID,
            CharacterController.velocity.magnitude, 1.0f, Time.deltaTime * 10.0f);
        Animator.SetFloat(_velocityXID,
            GetScaledMoveInput().x, 1.0f, Time.deltaTime * 10.0f);
        Animator.SetFloat(_velocityZID,
            GetScaledMoveInput().y, 1.0f, Time.deltaTime * 10.0f);
    }

    /// <summary>
    /// Translates strings into integers for more efficient comparison when 
    /// updating animator parameters.
    /// </summary>
    private void AssignAnimatorIDs()
    {
        IsCrouchedID = Animator.StringToHash("IsCrouched");
        IsGroundedID = Animator.StringToHash("IsGrounded");
        JumpTriggerID = Animator.StringToHash("JumpTrigger");
        _speedID = Animator.StringToHash("Speed");
        _velocityXID = Animator.StringToHash("VelocityX");
        _velocityZID = Animator.StringToHash("VelocityZ");
    }

    /// <summary>
    /// Apply scaled gravitational force to the airborn player.
    /// </summary>
    private void HandleGravity()
    {
        if (!GroundSensor.Active)
        {
            Movement += new Vector3(0.0f, Gravity * Time.deltaTime, 0.0f);
        }

        if (Movement.y < -MaxFallVelocity)
        {
            Movement.y = -MaxFallVelocity;
        }
    }

    public void UpdateCurrentStateString(PlayerBaseState state)
    {
        if (CurrentStateStringVariable == null)
        {
            return;
        }

        SubstateList.Add(state);
        if (state.CurrentSubState == null)
        {
            CurrentStateStringVariable.Value = "";
            for (int i = 0; i < SubstateList.Count; i++)
            {
                if (i != 0)
                {
                    CurrentStateStringVariable.Value +=
                        string.Format(" : {0}", SubstateList[i].Name);
                }
                else
                {
                    CurrentStateStringVariable.Value = SubstateList[i].Name;
                }
            }
        }
        else
        {
            UpdateCurrentStateString(state.CurrentSubState);
        }
    }
}
