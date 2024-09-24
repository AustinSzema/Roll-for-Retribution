using UnityEngine;

public class Dice : Magnetic
{
    
    private void Start()
    {
        rb.AddForce(Random.onUnitSphere * 100f);
    }
}