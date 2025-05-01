using UnityEngine;

/// <summary>
/// Concrete state class contianing the player's fall logic.
/// </summary>
public class PlayerFallState : PlayerBaseState
{
    /// <summary>
    /// Constructor for the PlayerFallState. Passes currentContext and
    /// playerStateFactory as arguments to the PlayerBaseState constructor.
    /// </summary>
    /// <param name="currentContext">Context to be passed to the PlayerBaseState 
    /// constructor.</param>
    /// <param name="playerStateFactory">PlayerStateFactory to be passed to the 
    /// PlayerBaseState constructor.</param>
    public PlayerFallState(PlayerStateMachine currentContext,
        PlayerStateFactory playerStateFactory) :
        base(currentContext, playerStateFactory)
    {
        Name = "Fall";
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
        return false;
    }

    public override void EnterState()
    {
        InitalizeSubstate();
        Context.Animator.SetBool(Context.IsGroundedID, false);
    }

    public override void ExitState()
    {

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
}
