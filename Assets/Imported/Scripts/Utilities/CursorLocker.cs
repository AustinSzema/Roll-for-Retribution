using UnityEngine;

/// <summary>
/// Controls the lock state of the player's cursor
/// </summary>
public class CursorLocker : MonoBehaviour
{
    /// <summary>
    /// Should the player's cursor be locked on start?
    /// </summary>
    [Tooltip("Should the player's cursor be locked on start?")]
    [SerializeField] private bool lockOnStart;

    /// <summary>
    /// Current state of the cursor lock. If true, the cursor is locked and
    /// invisible.
    /// </summary>
    private bool isLocked;

    #region MonoBehaviour Methods
    public void Start()
    {
        if (lockOnStart)
        {
            LockCursor();
        }
    }
    #endregion

    /// <summary>
    /// Locks the cursor and makes it invisible.
    /// </summary>
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isLocked = true;
    }

    public void ToggleCursorLock()
    {
        if (isLocked)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }
    }

    /// <summary>
    /// Unlocks the cursor and makes it visible.
    /// </summary>
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isLocked = false;
    }
}

