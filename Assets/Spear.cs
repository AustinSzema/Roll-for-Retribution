using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Magnetic
{
    // Update is called once per frame
    void Update()
    {
        // Get the rotation based on the camera's forward direction
        Quaternion targetRotation = Quaternion.LookRotation(Camera.main.transform.forward);

        // Apply the rotation to the spear and its Rigidbody
        transform.rotation = targetRotation;
        _rigidbody.rotation = targetRotation;

        Debug.Log("ROTATION CAMERA " + targetRotation);
    }
}