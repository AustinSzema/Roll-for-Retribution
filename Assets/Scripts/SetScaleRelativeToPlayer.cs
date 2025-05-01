using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScaleRelativeToPlayer : MonoBehaviour
{
    [SerializeField] private Vector3Variable _playerPos;

    private float minDistance = 3f; // Minimum distance for scaling to 0.5f
    private float maxDistance = 4f; // Maximum distance for scaling to 1f
    
    private void Update()
    {
        // Calculate the distance between the object and the player
        float distanceToPlayer = Vector3.Distance(transform.position, _playerPos.Value);

        // Calculate the scale factor based on the distance
        float scaleFactor = Mathf.Clamp01((distanceToPlayer - minDistance) / (maxDistance - minDistance));

        // Interpolate between 0.5f and 1f based on the distance
        float scaleValue = Mathf.Lerp(0.8f, 1f, scaleFactor);

        // Apply the new scale to the object
        transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }
}
