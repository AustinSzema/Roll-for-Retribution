using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatternShot : Weapon
{
    [SerializeField] private PatternSerializer pattern;
    
    public void Shoot(Vector3 magnetForwardDirection, int index)
    {
        Quaternion q = Quaternion.FromToRotation(Vector3.forward, magnetForwardDirection);
        rb.AddForce(q * pattern.PatternPoints[index%pattern.PatternPoints.Count].normalized * shootForce);
    }
}
