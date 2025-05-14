using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "Scriptable Objects/PlayerStatsSO")]
public class PlayerStatsSO : ScriptableObject
{
    
    [Header("Ground Movement")]
    public float moveSpeed = 100f;
    public float groundDrag = 5f;
    [Space]
    [Tooltip("Unused")] public float walkSpeed;
    [Tooltip("Unused")] public float sprintSpeed;
    
    [Header("Air Movement")]
    public float jumpForce = 30f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 1.5f;
    [Space]
    public float groundPoundForce = -240f;
    public float groundPoundCooldown = 1f;
    [Space]
    //public float gravityMultiplier = 2f;

    [Space]
    public float fallGravityMultiplier = 5f;
    public float lowJumpGravityMultiplier = 3f;
    public float baseGravity = -19.62f;

    
    
    [Header("Levitate Ability")] public float _maxFlightDuration = 150;
    [SerializeField] public float _fuelDecrementAmount = 0.75f;
    [SerializeField] public float _fuelRechargeAmount = 0.1f;
    public float _fuelPenaltyThreshold = 125;
    [Tooltip("Sets the Y value of player's velocity")] [SerializeField]
    public float _flightForce = 80f;


    
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode groundPoundKey = KeyCode.LeftShift;

}
