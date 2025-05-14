using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "Scriptable Objects/PlayerStatsSO")]
public class PlayerStatsSO : ScriptableObject
{
    
    [Header("Ground Movement")]
    public float moveSpeed = 30f;
    public float groundDrag = 5f;
    [Space]
    [Tooltip("Unused")] public float walkSpeed;
    [Tooltip("Unused")] public float sprintSpeed;
    
    [Header("Air Movement")]
    public float jumpForce = 30f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 1.5f;
    [Space]
    public float groundPoundForce = -60f;
    public float groundPoundCooldown = 0.25f;
    [Space]
    //public float gravityMultiplier = 2f;

    [Space]
    public float fallGravityMultiplier = 5f;
    public float lowJumpGravityMultiplier = 2f;
    public float baseGravity = -9.81f;

    
    
    [Header("Levitate Ability")] public float _maxFlightDuration = 10;
    [SerializeField] public float _fuelDecrementAmount = 1;
    [SerializeField] public float _fuelRechargeAmount = 1;
    public float _fuelPenaltyThreshold = 1;
    [Tooltip("Sets the Y value of player's velocity")] [SerializeField]
    public float _flightForce = 30f;


    
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode groundPoundKey = KeyCode.LeftControl;

}
