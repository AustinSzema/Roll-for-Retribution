using Cinemachine;
using UnityEngine;

/// <summary>
/// Assigns a transform for the attached Cinemachine Virtual Camera to follow
/// when the game starts. Make sure the transform to follow is stored in another
/// script's "Awake" call to prevent race condition issues.
/// </summary>
public class AssignCMVCamFollowTargetOnStart : MonoBehaviour
{
    /// <summary>
    /// Contains the transform reference the attached virtual camera should 
    /// follow.
    /// </summary>
    [Tooltip("Contains the transform reference the attached virtual " + 
        "camera should follow.")]
    [SerializeField] private TransformReferenceVariable transformToFollow;

    /// <summary>
    /// Cinemachine virtual camera component to which the follow target will be
    /// assigned.
    /// </summary>
    private CinemachineVirtualCamera virtualCamera;

    #region MonoBehaviour Methods
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void Start()
    {
        virtualCamera.Follow = transformToFollow.Value;
    }
    #endregion
}
