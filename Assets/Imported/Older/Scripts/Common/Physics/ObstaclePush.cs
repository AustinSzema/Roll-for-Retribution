using UnityEngine;

/// <summary>
/// Allows the attached CharacterController component to push obstacles.
/// </summary>
public class ObstaclePush : MonoBehaviour
{
    /// <summary>
    /// CharacterController used to push obstacles.
    /// </summary>
    [Tooltip("CharacterController used to push obstacles.")]
    [SerializeField] private CharacterController characterController;

    /// <summary>
    /// Rigidbody determining the mass of the pusher.
    /// </summary>
    [Tooltip("Rigidbody determining the mass of the pusher.")]
    [SerializeField] private Rigidbody rb;

    /// <summary>
    /// Float by which to scale push force.
    /// </summary>
    [Tooltip("Float by which to scale push force.")]
    [SerializeField] private float strengthMultiplier = 1.0f;

    #region MonoBehaviour Methods
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody targetRb = hit.collider.attachedRigidbody;

        if (targetRb != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - 
                transform.position;
            forceDirection.Normalize();

            targetRb.AddForceAtPosition(forceDirection *
                characterController.velocity.magnitude * rb.mass * 
                strengthMultiplier, transform.position);
        }
    }
    #endregion
}
