using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedShield : Weapon
{
    // Update is called once per frame
    private void SetRotation()
    {
        // Get the rotation based on the camera's forward direction
        Quaternion targetRotation = Quaternion.LookRotation(Camera.main.transform.forward);
    
        // Apply the rotation to the spear and its Rigidbody
        transform.rotation = targetRotation;
        rb.rotation = targetRotation;
        }
    
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
    

    private void Update()
    {
        AddWeight();
        SetRotation();
    }

    private void AddWeight()
    {
        rb.AddForce(new Vector3(0f, -5f, 1f));
    }
}
