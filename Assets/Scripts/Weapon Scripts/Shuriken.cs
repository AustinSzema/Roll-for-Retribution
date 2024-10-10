using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Weapon
{
    [Header("Totem specific fields")]
    [SerializeField] private float shrinkDistance = 2.0f; // Distance within which the totem shrinks
    [SerializeField] private float maxDistance = 30.0f;    // Max distance for scaling
    [SerializeField] private float minScale = 1f;        // Minimum scale when close
    [SerializeField] private float maxScale = 20.0f;       // Maximum scale when far away
    [SerializeField] private float lerpSpeed = 100.0f;     // Speed of scaling transition
    [SerializeField] private float rotationSpeed = 100f; // Speed of rotation on the Y axis


    public override void Shoot(Vector3 magnetForwardDirection)
    { 
        base.Shoot(magnetForwardDirection);
        rb.AddForce(magnetForwardDirection * shootForce);
        rb.AddTorque(transform.up * rotationSpeed);
    }

    private void Update()
    {
        // Get the distance from the totem to the hand position
        float distance = Vector3.Distance(transform.position, GameManager.Instance.handPosition);
        
        // Calculate the target scale based on distance
        float targetScale = distance <= shrinkDistance ? minScale : Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(shrinkDistance, maxDistance, distance));
    
        // Lerp the current scale to the target scale for smooth transition
        float currentScale = transform.localScale.x; // Assuming uniform scaling
        float newScale = Mathf.Lerp(currentScale, targetScale, Time.deltaTime * lerpSpeed);
        
        // Apply the new scale
        transform.localScale = new Vector3(newScale, newScale, newScale);
        
    }
}
