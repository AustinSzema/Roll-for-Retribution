using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullRing : Weapon
{
    public override void Shoot(Vector3 magnetForwardDirection)
    {
        rb.AddForce(shootForce * magnetForwardDirection);
    }
}
