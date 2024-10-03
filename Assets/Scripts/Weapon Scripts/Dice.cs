using UnityEngine;

public class Dice : Weapon
{
    
    private void Start()
    {
        rb.AddForce(Random.onUnitSphere * 100f);
    }
}