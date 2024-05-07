using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomainExpansionFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    private Vector3 localOffset = new Vector3(0f, 0f, 2f);
    void Update()
    {
        // Transform localOffset from local space to world space
        Vector3 worldOffset = _cameraTransform.TransformVector(localOffset);

        // Set the position in local space
        transform.position = _cameraTransform.position + worldOffset;
    }
}
