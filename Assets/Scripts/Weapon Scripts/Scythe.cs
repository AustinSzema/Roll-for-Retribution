using UnityEngine;
using Random = UnityEngine.Random;

public class Scythe : Weapon
{
    private int NegOnePosOne() // returns -1 or 1
    {
        return Random.Range(0, 1) * 2 - 1;
    }
    public override void Attract(Vector3 magnetPosition)
    {

        rb.linearVelocity = Vector3.zero;
        rb.position = Vector3.MoveTowards(rb.position, magnetPosition,
            Time.deltaTime * pullSpeed);
    }

    public virtual void Shoot(Vector3 magnetForwardDirection)
    {
        rb.AddTorque(100f * NegOnePosOne() * transform.right);
        rb.AddTorque(1000f * NegOnePosOne() * transform.up);
        
        rb.AddForce(shootForce * magnetForwardDirection);

    }


}
