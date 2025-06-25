using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats SO")]
    [field: SerializeField]
    public PlayerStatsSO playerStats { get; private set; }


    private bool readyToJump = true;
    private bool readyToGroundPound = true;
    private bool canGroundPound = true;

    [Header("Ground Check")] public float playerHeight = 1.5f;
    public LayerMask whatIsGround;
    private bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    [SerializeField] Rigidbody rb;

    [SerializeField] private AbilityList abilityList;

    
    
    public void ActivateAll()
    {
        foreach (var ability in abilityList._abilities)
        {
            ability.Activate(this);
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //readyToJump = true;
        ActivateAll();

        EyeballController.Instance.ModifyStats(this);
    }

    private void Update()
    {
        //Debug.Log("Velocity: " + rb.velocity.magnitude);
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.5f, whatIsGround);

        Debug.Log("Player is grounded: " + grounded);
        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
        {
            rb.linearDamping = playerStats.groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
    }


    private void FixedUpdate()
    {
        MovePlayer();
        AddGravity();
    }

    private void AddGravity()
    {
        // Custom gravity control
        if (rb.linearVelocity.y < 0) // Falling
        {
            rb.AddForce(playerStats.baseGravity * playerStats.fallGravityMultiplier * Vector3.up, ForceMode.Acceleration);
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(playerStats.jumpKey)) // Letting go of jump
        {
            rb.AddForce(playerStats.baseGravity * playerStats.lowJumpGravityMultiplier * Vector3.up, ForceMode.Acceleration);
        }
        else // Rising normally
        {
            rb.AddForce(Vector3.up * playerStats.baseGravity, ForceMode.Acceleration);
        }
    }


    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        // when to jump
        if (Input.GetKey(playerStats.jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), playerStats.jumpCooldown);
        }

        if (Input.GetKeyUp(playerStats.groundPoundKey))
        {
            canGroundPound = true;
        }

        // when to ground pound
        /*if (Input.GetKeyDown(playerStats.groundPoundKey) && canGroundPound && readyToGroundPound && !grounded)
        {
            canGroundPound = false;
            GroundPound();
            Invoke(nameof(ResetGroundPound), playerStats.groundPoundCooldown);
        }*/
    }


    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(playerStats.moveSpeed * 10f * moveDirection.normalized, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(playerStats.airMultiplier * playerStats.moveSpeed * 10f * moveDirection.normalized,
                ForceMode.Force);
    }


    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > playerStats.moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * playerStats.moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * playerStats.jumpForce, ForceMode.Impulse);
    }

    private void GroundPound()
    {
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * playerStats.groundPoundForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void ResetGroundPound()
    {
        readyToGroundPound = true;
        canGroundPound = true;
    }
    
    public void TemporarilySetVelocity(Vector3 newVelocity, float duration)
    {
        StopCoroutine(nameof(ResetVelocityAfterTime)); // in case it's already running
        rb.linearVelocity = newVelocity;
        StartCoroutine(ResetVelocityAfterTime(duration));
    }

    private IEnumerator ResetVelocityAfterTime(float time)
    {
        // Optionally disable movement while bouncing
        float originalSpeed = playerStats.moveSpeed;
        float originalAirMultiplier = playerStats.airMultiplier;

        // Temporarily disable speed control to preserve bounce momentum
        playerStats.moveSpeed = 0f;
        playerStats.airMultiplier = 0f;

        yield return new WaitForSeconds(time);

        // Restore original speed
        playerStats.moveSpeed = originalSpeed;
        playerStats.airMultiplier = originalAirMultiplier;
    }

}