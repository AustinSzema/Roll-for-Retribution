using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatternShot : Weapon
{
    [SerializeField] private PatternSerializer pattern;
    [SerializeField] private float rotationSpeeds = 0f; // Speed of rotation on the Y axis

    private Vector3 startingSize = Vector3.one;

    private void Start()
    {
        startingSize = transform.localScale;
    }

    public void Shoot(Vector3 magnetForwardDirection, int index)
    {
        Quaternion q = Quaternion.FromToRotation(Vector3.forward, magnetForwardDirection);
        rb.AddForce(q * pattern.PatternPoints[index % pattern.PatternPoints.Count].normalized * shootForce);
        rb.AddTorque(transform.up * rotationSpeeds);
    }

    [Header("Shrink Weapon In Hand Fields")] 
    [SerializeField] private bool shrinksWhenInHand = false; 
    [SerializeField] private float shrinkDistance = 2.0f; // Distance within which the totem shrinks
    [SerializeField] private float maxDistance = 30.0f;    // Max distance for scaling
    [SerializeField] private float minScaleFactor = 0.5f;        // Minimum scale when close
    [SerializeField] private float maxScaleFactor = 1f;       // Maximum scale when far away
    [SerializeField] private float lerpSpeed = 100.0f;     // Speed of scaling transition

    private void Update()
    {
        if (shrinksWhenInHand)
        {
            // Get the distance from the totem to the hand position
            float distance = Vector3.Distance(transform.position, GameManager.Instance.handPosition);
        
            // Calculate the target scale based on distance
            float targetScale = distance <= shrinkDistance ? minScaleFactor : Mathf.Lerp(minScaleFactor, maxScaleFactor, Mathf.InverseLerp(shrinkDistance, maxDistance, distance));

            // Lerp the current scale to the target scale for smooth transition
            float currentScaleFactor = transform.localScale.x / startingSize.x; // Determine the current scale factor relative to the starting size
            float newScaleFactor = Mathf.Lerp(currentScaleFactor, targetScale, Time.deltaTime * lerpSpeed);
        
            // Apply the new scale while maintaining the original proportions
            transform.localScale = startingSize * newScaleFactor;
        }
    }
}
