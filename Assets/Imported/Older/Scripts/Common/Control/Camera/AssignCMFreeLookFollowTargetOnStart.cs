using Cinemachine;
using UnityEngine;

/// <summary>
/// Assigns a transform for the attached Cinemachine Free Look Camera to follow
/// when the game starts. Make sure the transform to follow is stored in another
/// script's "Awake" call to prevent race condition issues.
/// </summary>
public class AssignCMFreeLookFollowTargetOnStart : MonoBehaviour
{
    /// <summary>
    /// Contains the transform reference the attached free look camera should 
    /// follow.
    /// </summary>
    [Tooltip("Contains the transform reference the attached free look " + 
        "camera should follow.")]
    [SerializeField] private TransformReferenceVariable transformToFollow;

    /// <summary>
    /// Cinemachine free look camera to which the follow target will be 
    /// assigned.
    /// </summary>
    private CinemachineFreeLook freeLookCamera;

    #region MonoBehaviour Methods
    private void Awake()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();
    }
    private void Start()
    {
        freeLookCamera.Follow = transformToFollow.Value;
    }
    #endregion
}
