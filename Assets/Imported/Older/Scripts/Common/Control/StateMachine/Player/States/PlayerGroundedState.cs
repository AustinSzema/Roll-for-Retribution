/// <summary>
/// Concrete state class contianing the player's grounded logic.
/// </summary>
public class PlayerGroundedState : PlayerBaseState
{
    /// <summary>
    /// Constructor for the PlayerGroundedState. Passes currentContext and
    /// playerStateFactory as arguments to the PlayerBaseState constructor.
    /// </summary>
    /// <param name="currentContext">Context to be passed to the PlayerBaseState 
    /// constructor.</param>
    /// <param name="playerStateFactory">PlayerStateFactory to be passed to the 
    /// PlayerBaseState constructor.</param>
    public PlayerGroundedState(PlayerStateMachine currentContext,
        PlayerStateFactory playerStateFactory) :
        base(currentContext, playerStateFactory)
    {
        Name = "Grounded";
        IsRootState = true;
    }

    #region PlayerBaseState Methods
    public override bool CheckSwitchStates()
    {
        if (Context.IsJumpPressed && !Context.RequireNewJumpPress && 
            !Context.IsSprinting)
        {
            SwitchState(Factory.Jump());
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
        Context.Animator.SetBool(Context.IsGroundedID, true);
    }

    public override void ExitState()
    {
        Context.CurrentSpeedMultiplier = Context.BaseSpeedMultipler;
    }

    public override void InitalizeSubstate()
    {
        if (Context.IsCrouchPressed)
        {
            SetSubState(Factory.Crouched());
        }
        else
        {
            SetSubState(Factory.Standing());
        }
    }

    public override void UpdateState()
    {
        StateUpdated = CheckSwitchStates();
    }
    #endregion
}
