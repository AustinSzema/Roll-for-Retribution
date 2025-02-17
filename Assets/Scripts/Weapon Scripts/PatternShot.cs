using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatternShot : Weapon
{
    [SerializeField] private PatternSerializer pattern;
    private Vector3 startingSize = Vector3.one;
    public bool IsBeingAttracted { get; private set; }
    
    [Header("Scaling and Rotating")] 
    [SerializeField] private bool shrinksWhenInHand = false;

    [SerializeField] private float shrinkDistance = 2.0f; // Distance within which the totem shrinks
    [SerializeField] private float maxDistance = 30.0f;    // Max distance for scaling
    [SerializeField] private float minScaleFactor = 0.5f;        // Minimum scale when close
    [SerializeField] private float maxScaleFactor = 1f;       // Maximum scale when far away
    [SerializeField] private float lerpSpeed = 100.0f;     // Speed of scaling transition
    [SerializeField] private float rotationSpeeds = 0f; // Speed of rotation on the Y axis

    [Header("Checks")]
    [SerializeField] private bool isSpear = false;
    [SerializeField] private bool freezeRotation = false;

    public void Start()
    {
        transform.localScale = startingSize;
    }

    public void Shoot(Vector3 magnetForwardDirection, int index)
    {
        //IsBeingAttracted = false;
        Quaternion q = Quaternion.LookRotation(magnetForwardDirection);
        Vector3 force = q * pattern.PatternPoints[index % pattern.PatternPoints.Count].normalized * shootForce;
        rb.AddForce(force);
        rb.AddTorque(transform.up * rotationSpeeds);
        if (!freezeRotation)
        {
            Quaternion targetRotation = Quaternion.LookRotation(force);
            rb.rotation = targetRotation; 
        }
    }
    
    private void Update()
    {
        if (shrinksWhenInHand)
        {
            // Get the distance from the totem to the hand position
            float distance = Vector3.Distance(transform.position, GameManager.Instance.handPosition);
        
            // Calculate the target scale based on distance
            float targetScale = distance <= shrinkDistance ? minScaleFactor : Mathf.Lerp(minScaleFactor, maxScaleFactor, Mathf.InverseLerp(shrinkDistance, maxDistance, distance));
    
            // Lerp the current scale to the target scale for smooth transition
            float currentScale = transform.localScale.x; // Assuming uniform scaling
            float newScale = Mathf.Lerp(currentScale, targetScale, Time.deltaTime * lerpSpeed);
        
            // Apply the new scale
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }

    /*private void FaceTowardsMovementDirection()
    {
        if(rb.velocity.sqrMagnitude > 1)
        {
            Quaternion q = Quaternion.LookRotation(rb.velocity);
            rb.rotation = q;
        }
    }*/

    /*private void OnCollisionExit(Collision other)
    {
        if (!IsBeingAttracted)
        {
            FaceTowardsMovementDirection();
        }
    }*/

    protected override void OnCollisionEnter(Collision other)
    {
        if (isSpear)
        {
            if (other.gameObject.CompareTag(TagManager.groundTag) || other.gameObject.CompareTag(TagManager.cageTag))
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }

            HitEnemy(other.gameObject);
        }
    }
    
    public override void Attract(Vector3 magnetPosition)
    {
        //IsBeingAttracted = true;

        if (isSpear)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.linearVelocity = Vector3.zero;
            rb.position = Vector3.MoveTowards(rb.position, magnetPosition,
                Time.deltaTime * pullSpeed);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
            rb.position = Vector3.MoveTowards(rb.position, magnetPosition,
                Time.deltaTime * pullSpeed);
        }
    }
}
