using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBall : Weapon
{
    private Vector3 minScale = Vector3.one * 0.5f; // Minimum scale
    private Vector3 maxScale = Vector3.one * 2.0f; // Maximum scale
    private float maxDistance = 10f; // The maximum distance at which the object reaches max scale
    private float minDistance = 1f; // The minimum distance at which the object reaches min scale
    private float outOfRangeScale = 10f; // Scale when out of range
    private float lerpSpeed = 5f; // Speed at which the scale lerps to outOfRangeScale

    void Update()
    {
        // Calculate distance from the GameManager's hand position
        float distance = Vector3.Distance(transform.position, GameManager.Instance.handPosition);

        Vector3 targetScale;

        if (distance > maxDistance)
        {
            // Gradually increase to outOfRangeScale when distance exceeds maxDistance
            targetScale = Vector3.one * outOfRangeScale;
        }
        else
        {
            // Clamp the distance within min and max distance range
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            // Calculate scale factor based on distance within range
            float scaleFactor = (distance - minDistance) / (maxDistance - minDistance);

            // Interpolate between minScale and maxScale based on the scale factor
            targetScale = Vector3.Lerp(minScale, maxScale, scaleFactor);
        }

        // Lerp the current scale towards the target scale
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * lerpSpeed);
    }
}
