using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRigidBody : MonoBehaviour
{
    [SerializeField] private Rigidbody platformRb;
    [SerializeField] private GameObject OriginObject;
    [SerializeField] private float rotationSpeed;


    public void Update()
    {
        rotateRigidBody(platformRb, OriginObject.transform.position, Vector3.up, rotationSpeed);
    }

    public void rotateRigidBody(Rigidbody rb, Vector3 origin, Vector3 axis, float angle)
    {
        Quaternion q = Quaternion.AngleAxis(angle, axis);
        rb.MovePosition(q * (rb.transform.position-origin) + origin);
        rb.MoveRotation(rb.transform.rotation * q);
    }
    
}
