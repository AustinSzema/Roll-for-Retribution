/// <summary>
/// Base abstract class for states in the PlayerStateMachine.
/// </summary>
public abstract class PlayerBaseState
{
    /// <summary>
    /// Name of this state.
    /// </summary>
    public string Name;

    /// <summary>
    /// Context using this base state.
    /// </summary>
    protected PlayerStateMachine Context;

    /// <summary>
    /// This state's current active sub state.
    /// </summary>
    public PlayerBaseState CurrentSubState;

    /// <summary>
    /// This state's current active super state.
    /// </summary>
    protected PlayerBaseState CurrentSuperState;

    /// <summary>
    /// Determines if this state is at the top of the hierarchical state 
    /// machine.
    /// </summary>
    protected bool IsRootState = false;

    /// <summary>
    /// Determines if the state has already been updated this tick.
    /// </summary>
    protected bool StateUpdated = false;

    /// <summary>
    /// PlayerStateFactory generating concrete states from this base state.
    /// </summary>
    protected PlayerStateFactory Factory;

    /// <summary>
    /// Constructor for the PlayerBaseState class. Stores the context using this
    /// base state and the PlayerStateFactory generating concrete states from 
    /// this base state as protected variables.
    /// </summary>
    /// <param name="currentContext">PlayerStateMachine context using this base
    /// state.</param>
    /// <param name="playerStateFactory">PlayerStateFactory generating concrete 
    /// states from this base state.</param>
    public PlayerBaseState(PlayerStateMachine currentContext,
        PlayerStateFactory playerStateFactory)
    {
        Context = currentContext;
        Factory = playerStateFactory;
    }
    /// <summary>
    /// Logic executed to check whether the context's current state needs to be
    /// switched.
    /// </summary>
    public abstract bool CheckSwitchStates();

    /// <summary>
    /// Logic executed when context enters this state.
    /// </summary>
    public abstract void EnterState();

    /// <summary>
    /// Logic executed when context exits this state.
    /// </summary>
    public abstract void ExitState();

    /// <summary>
    /// Initializes the substate of this state, if applicable.
    /// </summary>
    public abstract void InitalizeSubstate();

    /// <summary>
    /// Logic executed each frame the context is updating this state.
    /// </summary>
    public abstract void UpdateState();

    /// <summary>
    /// Enters this state. Then, enters this state's substate if applicable.
    /// </summary>
    public void EnterStates()
    {
        EnterState();
        if (CurrentSubState != null)
        {
            CurrentSubState.EnterState();
        }
    }

    /// <summary>
    /// Exits this state. Then, exits this state's substate if applicable.
    /// </summary>
    public void ExitStates()
    {
        ExitState();
        if (CurrentSubState != null)
        {
            CurrentSubState.ExitStates();
        }
    }

    /// <summary>
    /// Updates this state. Then, updates this state's substate if applicable.
    /// </summary>
    public void UpdateStates()
    {
        UpdateState();
        if (CurrentSubState != null && !StateUpdated)
        {
            CurrentSubState.UpdateStates();
        }
    }

    /// <summary>
    /// Sets the substate of this state equal to the passed PlayerBaseState.
    /// </summary>
    /// <param name="newSubState">The new substate for this state.</param>
    protected void SetSubState(PlayerBaseState newSubState)
    {
        CurrentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

    /// <summary>
    /// Sets the superstate of this state equal to the passed PlayerBaseState.
    /// </summary>
    /// <param name="newSuperState">THe new superstate for this state.</param>
    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        CurrentSuperState = newSuperState;
    }

    /// <summary>
    /// Exits the current state, enters the new state, and switches the
    /// context's current state to the new state.
    /// </summary>
    /// <param name="newState">State to switch to.</param>
    protected void SwitchState(PlayerBaseState newState)
    {
        // Exit the current states
        ExitStates();

        if (IsRootState)
        {
            // Switch the currentState of the context.
            Context.CurrentState = newState;
        }
        else
        {
            // If this is not a root state, it necessarily has a super state. 
            // Instead of switching the state of the context, we instead switch
            // the substate of this super state.
            CurrentSuperState.SetSubState(newState);
        }

        // Enter the new states
        newState.EnterStates();

        Context.SubstateList.Clear();
        Context.UpdateCurrentStateString(Context.CurrentState);
    }
}
