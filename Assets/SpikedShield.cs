using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedShield : Magnetic
{
    // // Update is called once per frame
    // void Update()
    // {
    //     // Get the rotation based on the camera's forward direction
    //     Quaternion targetRotation = Quaternion.LookRotation(Camera.main.transform.forward);
    //
    //     // Apply the rotation to the spear and its Rigidbody
    //     transform.rotation = targetRotation;
    //     rb.rotation = targetRotation;
    //
    //     Debug.Log("ROTATION CAMERA " + targetRotation);
    // }
    
    public override void Slam()
    {
        rb.velocity = Vector3.zero;
        // Use Quaternion.Euler to specify the rotation in degrees
        Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
        transform.rotation = rotation;
        rb.rotation = rotation;
        rb.AddForce(slamForce * Vector3.down);
    }


    public override void Shoot(Vector3 magnetForwardDirection)
    {
        rb.AddForce(shootForce * magnetForwardDirection);

    }

    public override void Attract(Vector3 magnetPosition)
    {
        
        // Get the rotation based on the camera's forward direction
        Quaternion targetRotation = Quaternion.LookRotation(Camera.main.transform.forward);
        
        // Apply the rotation to the spear and its Rigidbody
        transform.rotation = targetRotation;
        
        rb.velocity = Vector3.zero;
        rb.position = magnetPosition;
        transform.position = magnetPosition;
    }

    private void Update()
    {
        AddWeight();
    }

    private void AddWeight()
    {
        rb.AddForce(new Vector3(0f, -5f, 1f));
    }
}
