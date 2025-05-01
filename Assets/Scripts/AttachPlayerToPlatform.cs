using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayerToPlatform : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        AttachPlayer(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        AttachPlayer(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        DetachPlayer(collision);
    }

    private void AttachPlayer(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
           
            Vector3 playerVel = collision.gameObject.GetComponent<Rigidbody>().linearVelocity;
            //collision.gameObject.GetComponent<Rigidbody>().velocity += new Vector3(rb.velocity.x, 0f, 0f);
            collision.gameObject.GetComponent<Rigidbody>().linearVelocity = new Vector3(rb.linearVelocity.x, playerVel.y, playerVel.z);
        }
    }

    private void DetachPlayer(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }

}
