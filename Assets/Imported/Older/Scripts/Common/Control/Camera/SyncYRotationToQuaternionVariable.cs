using UnityEngine;

/// <summary>
/// Rotates the attached transform to align with an assigned Quaternion
/// ScriptableObject variable whenever that variable updates.
/// </summary>
public class SyncYRotationToQuaternionVariable : MonoBehaviour
{
    /// <summary>
    /// Quaternion variable to drive rotation syncing.
    /// </summary>
    [Tooltip("Quaternion varialbe to drive rotation syncing.")]
    [SerializeField] private QuaternionVariable syncVariable;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        syncVariable.VariableUpdated += UpdateRotation;
    }
    private void OnDisable()
    {
        syncVariable.VariableUpdated -= UpdateRotation;
    }
    #endregion

    /// <summary>
    /// Syncs the rotation of the attatched transform to that of the sync 
    /// variable.
    /// </summary>
    private void UpdateRotation()
    {
        transform.rotation =
            Quaternion.Euler(0.0f, syncVariable.Value.eulerAngles.y, 0.0f);
    }
}
