using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : Weapon
{
    
    private void Start()
    {
        rb.AddForce(Random.onUnitSphere * 100f);
    }

    private void Update()
    {
        OnUpdate();
        float distance = Vector3.Distance(transform.position, GameManager.Instance.handPosition);

        if (distance < GameManager.Instance.snapRangeAroundHand) // TODO: this is dumb
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
