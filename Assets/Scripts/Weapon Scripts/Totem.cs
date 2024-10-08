using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : Weapon
{
    // Define the threshold distances
    private float shrinkDistance = 2.0f; // Distance within which the totem shrinks
    private float maxDistance = 30.0f;    // Max distance for scaling
    private float minScale = 0.5f;         // Minimum scale when close
    private float maxScale = 10.0f;         // Maximum scale when far away
    private float lerpSpeed = 100.0f;        // Speed of lerping

    private void Update()
    {
        // Get the distance from the totem to the hand position
        float distance = Vector3.Distance(transform.position, GameManager.Instance.handPosition);
        
        // Calculate the target scale based on distance
        float targetScale;

        if (distance <= shrinkDistance)
        {
            // Shrink to minScale if within shrinkDistance
            targetScale = minScale;
        }
        else
        {
            // Otherwise, scale based on the distance, clamped to a maximum scale
            float scaleFactor = Mathf.InverseLerp(shrinkDistance, maxDistance, distance);
            targetScale = Mathf.Lerp(minScale, maxScale, scaleFactor);
        }

        // Lerp the current scale to the target scale for smooth transition
        float currentScale = transform.localScale.x; // Assuming uniform scaling
        float newScale = Mathf.Lerp(currentScale, targetScale, Time.deltaTime * lerpSpeed);
        
        // Apply the new scale
        transform.localScale = new Vector3(newScale, newScale, newScale);
        
    }
}