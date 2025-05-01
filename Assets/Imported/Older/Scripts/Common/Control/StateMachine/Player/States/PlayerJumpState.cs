using UnityEngine;

/// <summary>
/// Concrete state class contianing the player's jump logic.
/// </summary>
public class PlayerJumpState : PlayerBaseState
{
    /// <summary>
    /// Constructor for the PlayerJumpState. Passes currentContext and
    /// playerStateFactory as arguments to the PlayerBaseState constructor.
    /// </summary>
    /// <param name="currentContext">Context to be passed to the PlayerBaseState 
    /// constructor.</param>
    /// <param name="playerStateFactory">PlayerStateFactory to be passed to the 
    /// PlayerBaseState constructor.</param>
    public PlayerJumpState(PlayerStateMachine currentContext,
        PlayerStateFactory playerStateFactory) :
        base(currentContext, playerStateFactory)
    {
        Name = "Jump";
        IsRootState = true;
    }
    #region PlayerBaseState Methods
    public override bool CheckSwitchStates()
    {
        if (Context.GroundSensor.Active)
        {
            SwitchState(Factory.Grounded());
            return true;
        }
        else if (!Context.GroundSensor.Active && Context.Movement.y < 0)
        {
            SwitchState(Factory.Fall());
            return true;
        }
        return false;
    }

    public override void EnterState()
    {
        InitalizeSubstate();
        HandleJump();
    }

    public override void ExitState()
    {
        if (Context.IsJumpPressed)
        {
            Context.RequireNewJumpPress = true;
        }
    }

    public override void InitalizeSubstate()
    {
        if (Context.MoveInput == Vector2.zero)
        {
            SetSubState(Factory.Idle());
        }
        else
        {
            if (Context.IsRunning)
            {
                SetSubState(Factory.Run());
            }
            else
            {
                SetSubState(Factory.Walk());
            }
        }
    }

    public override void UpdateState()
    {
        StateUpdated = CheckSwitchStates();
    }
    #endregion

    /// <summary>
    /// Give the player an upward velocity, simulating a jump.
    /// </summary>
    private void HandleJump()
    {
        float jumpVelocity =
            Mathf.Sqrt(Context.JumpHeight * 2 * -Context.Gravity);
        Context.Movement.y = jumpVelocity;
        Context.Animator.SetTrigger(Context.JumpTriggerID);
        Context.GroundSensor.DisabledTimer = Context.JumpDisableGroundSensorTime;
        Context.Animator.SetBool(Context.IsGroundedID, false);
    }
}
