using UnityEngine;

/// <summary>
/// Stores the attached transform to an asset level storage variable for
/// decoupling purposes.
/// </summary>
public class StoreTransformOnAwake : MonoBehaviour
{
    /// <summary>
    /// Variable in which to store the attached transform.
    /// </summary>
    [Tooltip("Variable in which to store the attached transform.")]
    [SerializeField] private TransformReferenceVariable storageVariable;

    #region MonoBehaviour Methods
    private void Awake()
    {
        storageVariable.Value = transform;
    }
    #endregion
}
