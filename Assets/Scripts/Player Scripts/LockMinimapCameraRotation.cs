using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMinimapCameraRotation : MonoBehaviour
{
    private Quaternion _originalRotation;

    private float _originalX;
    private float _originalY;
    private float _originalZ;
    private float _originalW;
        
    private void Start()
    {
        _originalRotation = transform.rotation;
        _originalX = _originalRotation.x;
        _originalY = _originalRotation.y;
        _originalZ = _originalRotation.z;
        _originalW = _originalRotation.w;
    }


    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(90, _originalY, transform.rotation.z, _originalW);
    }
}
