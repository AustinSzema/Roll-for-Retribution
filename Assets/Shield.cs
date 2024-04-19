using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float moveSpeed = 20f;
    
    private void Start()
    {
        rb.rotation = Camera.main.transform.rotation;
        rb.velocity = rb.transform.forward * moveSpeed;
    }

}
