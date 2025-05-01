using UnityEngine;

/// <summary>
/// Concrete state class contianing the player's run logic.
/// </summary>
public class PlayerRunState : PlayerBaseState
{
    /// <summary>
    /// Constructor for the PlayerRunState. Passes currentContext and
    /// playerStateFactory as arguments to the PlayerBaseState constructor.
    /// </summary>
    /// <param name="currentContext">Context to be passed to the PlayerBaseState 
    /// constructor.</param>
    /// <param name="playerStateFactory">PlayerStateFactory to be passed to the 
    /// PlayerBaseState constructor.</param>
    public PlayerRunState(PlayerStateMachine currentContext,
        PlayerStateFactory playerStateFactory) :
        base(currentContext, playerStateFactory)
    {
        Name = "Run";
    }

    #region PlayerBaseState Methods
    public override bool CheckSwitchStates()
    {
        if (Context.MoveInput == Vector2.zero)
        {
            SwitchState(Factory.Idle());
            return true;
        }
        else
        {
            if (Context.IsSprinting)
            {
                SwitchState(Factory.Sprint());
                return true;
            }
            else if (!Context.IsRunning)
            {
                SwitchState(Factory.Walk());
                return true;
            }
        }
        return false;
    }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void InitalizeSubstate()
    {

    }

    public override void UpdateState()
    {
        HandleXZMovement();
        StateUpdated = CheckSwitchStates();
    }
    #endregion

    /// <summary>
    /// Calculates the new movement speed of the character based on its walk 
    /// speed and acceleration parameters, updating the x and z components of 
    /// the final movement vector.
    /// </summary>
    private void HandleXZMovement()
    {
        if (Context.Acceleration != -1 && Context.Acceleration > 0)
        {
            float currentSpeed =
                new Vector3(Context.CharacterController.velocity.x, 0.0f,
                Context.CharacterController.velocity.z).magnitude;

            if (currentSpeed < Context.RunSpeed - 0.1f ||
            currentSpeed > Context.RunSpeed + 0.1f)
            {
                Context.NewSpeed = Mathf.Lerp(currentSpeed, Context.RunSpeed,
                    Context.Acceleration * Time.deltaTime);
            }
            else
            {
                Context.NewSpeed = Context.RunSpeed;
            }
        }
        else
        {
            Context.NewSpeed = Context.RunSpeed;
        }

        Vector3 xzMovement;

        if (Context.IsGamepadInput)
        {
            // Gamepad input is analog - character should always be running, 
            // and the move input itself will control the character's speed.
            xzMovement = ((Context.transform.forward * Context.MoveInput.y) +
                (Context.transform.right * Context.MoveInput.x)) *
                Context.NewSpeed * Context.CurrentSpeedMultiplier;
        }
        else
        {
            xzMovement = ((Context.transform.forward * Context.MoveInput.y) +
                (Context.transform.right * Context.MoveInput.x)).normalized *
                Context.NewSpeed;
        }
        Context.Movement.x = xzMovement.x;
        Context.Movement.z = xzMovement.z;
    }
}
