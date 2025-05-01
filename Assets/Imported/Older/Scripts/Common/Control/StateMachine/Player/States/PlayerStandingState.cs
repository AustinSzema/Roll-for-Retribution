using UnityEngine;

/// <summary>
/// Concrete state class contianing the player's standing logic.
/// </summary>
public class PlayerStandingState : PlayerBaseState
{
    /// <summary>
    /// Constructor for the PlayerStandingState. Passes currentContext and
    /// playerStateFactory as arguments to the PlayerBaseState constructor.
    /// </summary>
    /// <param name="currentContext">Context to be passed to the PlayerBaseState 
    /// constructor.</param>
    /// <param name="playerStateFactory">PlayerStateFactory to be passed to the 
    /// PlayerBaseState constructor.</param>
    public PlayerStandingState(PlayerStateMachine currentContext,
        PlayerStateFactory playerStateFactory) :
        base(currentContext, playerStateFactory)
    {
        Name = "Standing";
    }

    #region PlayerBaseState Methods
    public override bool CheckSwitchStates()
    {
        if (Context.IsCrouchPressed && (!Context.IsSprinting))
        {
            SwitchState(Factory.Crouched());
            return true;
        }
        return false;
    }

    public override void EnterState()
    {
        InitalizeSubstate();
        Context.CanSprint = true;
        Context.CurrentSpeedMultiplier = Context.BaseSpeedMultipler;
    }

    public override void ExitState()
    {
        Context.CanSprint = false;
    }

    public override void InitalizeSubstate()
    {
        if (Context.MoveInput == Vector2.zero)
        {
            SetSubState(Factory.Idle());
        }
        else
        {
            if (Context.IsSprinting)
            {
                SetSubState(Factory.Sprint());
            }
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
}
