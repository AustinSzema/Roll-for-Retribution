using UnityEngine;

/// <summary>
/// Keeps a child transform's original rotation, ignoring that of the parent.
/// </summary>
public class IgnoreParentRotation : MonoBehaviour
{
    /// <summary>
    /// Stores the transform's parent's rotation from the previous frame.
    /// </summary>
    private Quaternion _lastParentRotation;

    #region MonoBehaviour Methods
    void Start()
    {
        _lastParentRotation = transform.parent.localRotation;
    }

    void Update()
    {
        transform.localRotation = 
            Quaternion.Inverse(transform.parent.localRotation) * 
            _lastParentRotation * transform.localRotation;

        _lastParentRotation = transform.parent.localRotation;
    }
    #endregion
}
