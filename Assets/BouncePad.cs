using System;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagManager.playerTag))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            PlayerController pc = other.GetComponent<PlayerController>();

            if (pc && rb)
            {
                Vector3 bounceVelocity = Vector3.up * 70f + pc.orientation.transform.forward * 500f;
                
                pc.TemporarilySetVelocity(rb.linearVelocity + bounceVelocity, 0.2f);
                Debug.Log("Bounce velocity applied: " + bounceVelocity);
            }
            else
            {
                Debug.LogWarning("No playercontroller on player for bounce pad");
            }
        }

        
    }
}
