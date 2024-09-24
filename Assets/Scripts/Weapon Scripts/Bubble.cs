using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Magnetic
{
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, GameManager.Instance.handPosition);
        float clampedDistance = Mathf.Clamp(distance, 1, 30);
        transform.localScale = new Vector3(clampedDistance, clampedDistance, clampedDistance);
    }
}
