using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xpand : Weapon
{
    [SerializeField] private float expandSize = 10f;

    [SerializeField] private float rotationSpeed = 100f;

    [SerializeField] private float upwardsShootForce = 100f;

    private float sizeInHand = 5f;
    
    private bool inHand = false;
    
    // [SerializeField] private GameObject[] objectsToExpand;
    //
    // private List<Vector3> scales = new List<Vector3>();
    //
    // private void Start()
    // {
    //     foreach (GameObject obj in objectsToExpand)
    //     {
    //         scales.Add(obj.transform.localScale);
    //     }
    // }


    private Vector3 startSize;
    
    private void Start()
    {
        startSize = transform.localScale;
        transform.localScale /= sizeInHand;
    }

    public override void Shoot(Vector3 magnetForwardDirection)
    {
        rb.AddForce(Vector3.up * upwardsShootForce);
        rb.AddForce(magnetForwardDirection * shootForce);
        
        inHand = false;
    }
    
    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        Expand(other.gameObject);
    }

    
    
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Expand(other.gameObject);
    }

    private void Expand(GameObject other)
    {
        if (!other.CompareTag(TagManager.playerTag) && !inHand)
        {
            transform.localScale = startSize * expandSize;
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            /*
            foreach (GameObject obj in objectsToExpand)
            {
                Vector3 scale = obj.transform.localScale;
                obj.transform.localScale = new Vector3(scale.x, expandSize, scale.z);

            }
        */
        }
    }

    
    public override void Attract(Vector3 magnetPosition)
    {
        base.Attract(magnetPosition);
        rb.constraints = RigidbodyConstraints.None;
        transform.localScale = Vector3.one / sizeInHand;
        
        /*for (int i = 0; i < objectsToExpand.Length; i++)
        {
            objectsToExpand[i].transform.localScale = scales[i];
        }*/
        
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        inHand = true;
    }
    
}

