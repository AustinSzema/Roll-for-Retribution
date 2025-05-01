using UnityEngine;

/// <summary>
/// Concrete state class contianing the player's idle logic.
/// </summary>
public class PlayerIdleState : PlayerBaseState
{
    /// <summary>
    /// Constructor for the PlayerIdleState. Passes currentContext and
    /// playerStateFactory as arguments to the PlayerBaseState constructor.
    /// </summary>
    /// <param name="currentContext">Context to be passed to the PlayerBaseState 
    /// constructor.</param>
    /// <param name="playerStateFactory">PlayerStateFactory to be passed to the 
    /// PlayerBaseState constructor.</param>
    public PlayerIdleState(PlayerStateMachine currentContext,
        PlayerStateFactory playerStateFactory) :
        base(currentContext, playerStateFactory)
    {
        Name = "Idle";
    }
    #region PlayerBaseState Methods
    public override bool CheckSwitchStates()
    {
        if (Context.MoveInput != Vector2.zero)
        {
            if (Context.IsSprinting)
            {
                SwitchState(Factory.Sprint());
                return true;
            }
            else
            {
                if (Context.IsRunning)
                {
                    SwitchState(Factory.Run());
                    return true;
                }
                else
                {
                    SwitchState(Factory.Walk());
                    return true;
                }
            }
        }
        return false;
    }

    public override void EnterState()
    {
        Context.NewSpeed = 0;
    }

    public override void ExitState()
    {

    }

    public override void InitalizeSubstate()
    {

    }

    public override void UpdateState()
    {
        StateUpdated = CheckSwitchStates();
    }
    #endregion
}
