using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGravity : MonoBehaviour
{
    [SerializeField] private float _gravityMultiplier = 2f;
 
    [SerializeField] private Rigidbody rb;

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * _gravityMultiplier, ForceMode.Acceleration);
    }
}
