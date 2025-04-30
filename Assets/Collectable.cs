using UnityEngine;

[CreateAssetMenu(fileName = "Collectable", menuName = "Scriptable Objects/Collectable")]
public class Collectable : ScriptableObject
{
    [Header("Attacking")]
    public float shootForce = 6000f;
    public float slamForce = 5500f;
    public float pullSpeed = 60f;
    public float damage = 1;
    public float _slamCooldown = 3.0f;
    public float _shotgunCooldown = 3.0f;
    public float scale = 0f;
    
    [Header("Levitate Ability")]
    public float _maxFlightDuration = 10;
    public float _fuelDecrementAmount = 1;
    public float _fuelRechargeAmount = 1;
    public float _flightForce = 30f;
    public float _fuelPenaltyThreshold = 1;

    
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float walkSpeed;
    public float sprintSpeed;
    public float gravityMultiplier = 2f;


    [Header("Visual")] public Material eyeMaterial;
}
