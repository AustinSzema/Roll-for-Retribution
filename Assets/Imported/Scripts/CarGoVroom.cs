using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGoVroom : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Camera mainCam;

    [SerializeField] private float moveSpeed;
    
    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocity.magnitude < 1000f)
        {
            rb.AddForce(Input.GetAxisRaw("Vertical") * moveSpeed * mainCam.transform.forward);
        }

        rb.AddTorque(new Vector3(0f, Input.GetAxis("Horizontal"), 0f));
    }
}
