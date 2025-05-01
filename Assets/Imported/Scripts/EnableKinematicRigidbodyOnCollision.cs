using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableKinematicRigidbodyOnCollision : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            rb.isKinematic = true;
            col.enabled = false;
        }
    }
}
