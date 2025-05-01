using Cinemachine;
using UnityEngine;

/// <summary>
/// Assigns a transform for the attached Cinemachine Free Look Camera to look at
/// when the game starts. Make sure the transform to look at is stored in 
/// another script's "Awake" call to prevent race condition issues.
/// </summary>
public class AssignCMFreeLookLookAtTargetOnStart : MonoBehaviour
{
    /// <summary>
    /// Contains the transform reference the attached free look camera should 
    /// look at.
    /// </summary>
    [Tooltip("Contains the transform reference the attached free look " + 
        "camera should look at.")]
    [SerializeField] private TransformReferenceVariable transformToLookAt;

    /// <summary>
    /// Cinemachine free look camera to which the look at target will be 
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
        freeLookCamera.LookAt = transformToLookAt.Value;
    }
    #endregion
}
