using System.Collections.Generic;

/// <summary>
/// Enum storing all player states for cleanliness in reference.
/// </summary>
public enum PlayerState
{
    Crouched, Fall, Grounded, Idle, Jump, Run, Sprint, Standing, Walk
}

/// <summary>
/// Creates a reference to all player states and stores them in a dictionary.
/// Has public methods to access these state references.
/// </summary>
public class PlayerStateFactory
{
    /// <summary>
    /// Context for which this factory will generate states.
    /// </summary>
    private PlayerStateMachine _context;

    /// <summary>
    /// Stores state references for ease of access.
    /// </summary>
    private Dictionary<PlayerState,PlayerBaseState> _states = 
        new Dictionary<PlayerState,PlayerBaseState>();

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
        _states[PlayerState.Crouched] = new PlayerCrouchedState(_context, this);
        _states[PlayerState.Fall] = new PlayerFallState(_context, this);
        _states[PlayerState.Grounded] = new PlayerGroundedState(_context, this);
        _states[PlayerState.Idle] = new PlayerIdleState(_context, this);
        _states[PlayerState.Jump] = new PlayerJumpState(_context, this);
        _states[PlayerState.Run] = new PlayerRunState(_context, this);
        _states[PlayerState.Sprint] = new PlayerSprintState(_context, this);
        _states[PlayerState.Standing] = new PlayerStandingState(_context, this);
        _states[PlayerState.Walk] = new PlayerWalkState(_context, this);
    }

    public PlayerBaseState Crouched()
    {
        return _states[PlayerState.Crouched];
    }

    public PlayerBaseState Fall()
    {
        return _states[PlayerState.Fall];
    }

    public PlayerBaseState Grounded()
    {
        return _states[PlayerState.Grounded];
    }

    public PlayerBaseState Idle()
    {
        return _states[PlayerState.Idle];
    }

    public PlayerBaseState Jump()
    {
        return _states[PlayerState.Jump];
    }

    public PlayerBaseState Run()
    {
        return _states[PlayerState.Run];
    }

    public PlayerBaseState Sprint()
    {
        return _states[PlayerState.Sprint];
    }

    public PlayerBaseState Standing()
    {
        return _states[PlayerState.Standing];
    }

    public PlayerBaseState Walk()
    {
        return _states[PlayerState.Walk];
    }
}
