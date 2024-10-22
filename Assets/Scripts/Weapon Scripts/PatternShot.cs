using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatternShot : Weapon
{
    [SerializeField] private PatternSerializer pattern;
    [SerializeField] private float rotationSpeeds = 0f; // Speed of rotation on the Y axis
    

    
    public void Shoot(Vector3 magnetForwardDirection, int index)
    {
        Quaternion q = Quaternion.FromToRotation(Vector3.forward, magnetForwardDirection);
        rb.AddForce(q * pattern.PatternPoints[index%pattern.PatternPoints.Count].normalized * shootForce);
        rb.AddTorque(transform.up * rotationSpeeds);
    }
    
}


