using System.Collections;
using UnityEngine;

/// <summary>
/// Concrete state class contianing the player's crouched logic.
/// </summary>
public class PlayerCrouchedState : PlayerBaseState
{
    /// <summary>
    /// Constructor for the PlayerCrouchedState. Passes currentContext and
    /// playerStateFactory as arguments to the PlayerBaseState constructor.
    /// </summary>
    /// <param name="currentContext">Context to be passed to the PlayerBaseState 
    /// constructor.</param>
    /// <param name="playerStateFactory">PlayerStateFactory to be passed to the 
    /// PlayerBaseState constructor.</param>
    public PlayerCrouchedState(PlayerStateMachine currentContext,
        PlayerStateFactory playerStateFactory) :
        base(currentContext, playerStateFactory)
    {
        Name = "Crouched";
    }

    #region PlayerBaseState Methods
    public override bool CheckSwitchStates()
    {
        if (!Context.IsCrouchPressed && !Context.HeadSensor.Active)
        {
            SwitchState(Factory.Standing());
            return true;
        }
        return false;
    }

    public override void EnterState()
    {
        InitalizeSubstate();
        Context.CurrentSpeedMultiplier = Context.CrouchSpeedMultiplier;
        HandleCrouch();
    }

    public override void ExitState()
    {
        HandleUncrouch();
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
    /// Smoothly changes the bounds of the characterController to their
    /// opposite state.
    /// </summary>
    private IEnumerator CrouchUncrouchRoutine(bool isCrouching)
    {
        float elapsedTime = 0.0f;

        // Inital values
        Vector3 currentCenter = Context.CharacterController.center;
        float currentHeight = Context.CharacterController.height;
        float currentRadius = Context.CharacterController.radius;

        // Determine target values
        Vector3 targetCenter = isCrouching ? Context.CrouchCenter : Context.StandCenter;
        float targetHeight = isCrouching ? Context.CrouchHeight : Context.StandHeight;
        float targetRadius = isCrouching ? Context.CrouchRadius : Context.StandRadius;

        // Lerp between current and target values
        while (elapsedTime < Context.CrouchTime)
        {
            Context.CharacterController.center = Vector3.Lerp(currentCenter,
                targetCenter, elapsedTime / Context.CrouchTime);
            Context.CharacterController.height = Mathf.Lerp(currentHeight,
                targetHeight, elapsedTime / Context.CrouchTime);
            Context.CharacterController.radius = Mathf.Lerp(currentRadius,
                targetRadius, elapsedTime / Context.CrouchTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure target values are achieved
        Context.CharacterController.center = targetCenter;
        Context.CharacterController.height = targetHeight;
        Context.CharacterController.radius = targetRadius;
    }

    /// <summary>
    /// Crouches the character, adjusting the character's characterController
    /// bounds accordingly.
    /// </summary>
    private void HandleCrouch()
    {
        Context.Animator.SetBool(Context.IsCrouchedID, true);

        if (Context.ActiveRoutine != null)
        {
            Context.StopCoroutine(Context.ActiveRoutine);
        }

        Context.ActiveRoutine =
            Context.StartCoroutine(CrouchUncrouchRoutine(true));
    }

    /// <summary>
    /// Crouches the character, adjusting the character's characterController
    /// bounds accordingly.
    /// </summary>
    private void HandleUncrouch()
    {
        Context.Animator.SetBool(Context.IsCrouchedID, false);

        if (Context.ActiveRoutine != null)
        {
            Context.StopCoroutine(Context.ActiveRoutine);
        }

        Context.ActiveRoutine =
            Context.StartCoroutine(CrouchUncrouchRoutine(false));
    }
}
