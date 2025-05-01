using UnityEngine;

/// <summary>
/// Disables vsync and sets the application's target frame rate to the specified
/// count.
/// </summary>
public class SetTargetFrameRate : MonoBehaviour
{
    /// <summary>
    /// Frames per second value at which the game will be capped.
    /// </summary>
    [Tooltip("Frames per second value at which the game will be capped.")]
    [SerializeField] private int targetFramesPerSecond = 60;

    #region MonoBehaviour Methods
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFramesPerSecond;
    }
    #endregion
}
