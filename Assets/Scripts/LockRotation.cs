using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion _originalRotation;

    private void Start()
    {
        _originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = _originalRotation;
    }
}
