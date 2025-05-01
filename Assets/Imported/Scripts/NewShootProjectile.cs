using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 50f;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private AudioClip sfx1;
    [SerializeField] private AudioClip sfx2;
    [SerializeField] private AudioClip sfx3;
    [SerializeField] private AudioClip sfx4;
    
    
    

    void Update()
    {
        int vaRandom = UnityEngine.Random.Range(1, 4);
        if (Input.GetMouseButtonDown(0))
        {
                GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
                projectile.GetComponent<Rigidbody>().linearVelocity = shootingPoint.forward * projectileSpeed;
                projectile.transform.forward = shootingPoint.transform.forward;
                projectile.transform.Rotate(0f, 180f, 0f);
                projectile.transform.position += new Vector3(0f, 0.5f, 0f);
            if (vaRandom == 1) { 
                AudioSource.PlayClipAtPoint(sfx1, transform.position);
            }
            if (vaRandom == 2)
            {
                AudioSource.PlayClipAtPoint(sfx2, transform.position);
            }
            if (vaRandom == 3)
            {
                AudioSource.PlayClipAtPoint(sfx3, transform.position);
            }
            if (vaRandom == 4)
            {
                AudioSource.PlayClipAtPoint(sfx4, transform.position);
            }
        }
    }
}
