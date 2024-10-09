using System;
using System.Collections;
using UnityEngine;

public class Bubble : Weapon
{
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, GameManager.Instance.handPosition);
        float clampedDistance = Mathf.Clamp(distance, 1, 30);
        transform.localScale = new Vector3(clampedDistance, clampedDistance, clampedDistance);
 
        if (distance < GameManager.Instance.snapRangeAroundHand) // TODO: this is dumb
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    
}
