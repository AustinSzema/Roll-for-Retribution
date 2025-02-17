using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedShield : Weapon
{
    private bool rotateTowardsPlayerAndLock = true;
    private void SetRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(Camera.main.transform.forward);
        transform.rotation = targetRotation;
        rb.rotation = targetRotation;
    }
    
    public override void Attract(Vector3 magnetPosition)
    {
        rb.linearVelocity = Vector3.zero;
        rb.position = Vector3.MoveTowards(rb.position, magnetPosition,
            Time.deltaTime * pullSpeed);
        rotateTowardsPlayerAndLock = true;
    }

    public override void Slam()
    {
        rb.linearVelocity = Vector3.zero;
        // Use Quaternion.Euler to specify the rotation in degrees
        Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
        transform.rotation = rotation;
        rb.rotation = rotation;
        rb.AddForce(slamForce * Vector3.down);
    }


    public override void Shoot(Vector3 magnetForwardDirection)
    {
        rb.AddForce(shootForce * magnetForwardDirection);
        rotateTowardsPlayerAndLock = false;
        rb.linearVelocity = Vector3.zero;
        // Use Quaternion.Euler to specify the rotation in degrees
        Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
        transform.rotation = rotation;
        rb.rotation = rotation;
    }
    

    private void Update()
    {
        //AddWeight();
        if (rotateTowardsPlayerAndLock)
        {
            SetRotation();
        }
    }

    /*private void AddWeight()
    {
        rb.AddForce(new Vector3(0f, -5f, 1f));
    }*/
}
