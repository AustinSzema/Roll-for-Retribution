using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private Transform _portal;
    [SerializeField] private Transform _linkedPortal;

    private void Update()
    {
        Vector3 playerOffsetFromPortal = _playerCamera.position - _linkedPortal.position;
        transform.position = _portal.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(_portal.rotation, _linkedPortal.rotation);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * _playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
