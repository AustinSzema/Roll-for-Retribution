using Cinemachine;
using UnityEngine;

/// <summary>
/// Retrieves the horizontal and vertical axis values of the attached
/// CinemachinePOV component and stores them as a rotation at the asset level
/// in the assgined storage variable quaternion.
/// </summary>
public class StoreCMVcamRotation : MonoBehaviour
{
    /// <summary>
    /// Quaternion variable to store rotation in.
    /// </summary>
    [Tooltip("Quaternion variable to store rotation in.")]
    [SerializeField] private QuaternionVariable storageVariable;

    /// <summary>
    /// CinemachinePOV component from which horizontal and vertical axis
    /// values are retrieved.
    /// </summary>
    private CinemachinePOV aimPOV;

    #region MonoBehaviour Methods
    private void Awake()
    {
        aimPOV =  GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
    }
    private void Update()
    {
        storageVariable.Value = Quaternion.Euler(aimPOV.m_VerticalAxis.Value, 
            aimPOV.m_HorizontalAxis.Value, 0.0f);
    }
    #endregion
}
