using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Weapon
{
    [SerializeField] GameObject smallProjectilePrefab;
    [SerializeField] private int numberofSchrapnelObjects = 10;
    [SerializeField] private float explosionForce = 10f; 
    [SerializeField] private float despawnTime = 3f;
    [SerializeField] private float explosionDelayTime = 3f;

    private bool hasExploded = false;
    
    public override void Shoot(Vector3 magnetForwardDirection)
    { 
        base.Shoot(magnetForwardDirection);
        hasExploded = false; 
    }

    public override void Attract(Vector3 magnetPosition)
    {
        if(!hasExploded)
        base.Attract(magnetPosition);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded && collision.gameObject.CompareTag("Ground")) 
        {
            StartCoroutine(ExplosionDelay(explosionDelayTime));
            //Destroy(gameObject);
        }
    }

    void Explode()
    {
        // Create multiple smaller projectiles in random directions
        for (int i = 0; i < numberofSchrapnelObjects; i++) // Use the specified number of small projectiles
        {
            // Instantiate the small projectile at the current position with no rotation
            GameObject smallProjectile = Instantiate(smallProjectilePrefab, transform.position, Quaternion.identity);
            Rigidbody projectileRb = smallProjectile.GetComponent<Rigidbody>();

            if (projectileRb != null)
            {
                // Generate a random direction for each small projectile
                Vector3 randomDirection = GetRandomDirection();
                projectileRb.AddForce(randomDirection * explosionForce, ForceMode.Impulse);
            }

            // Start a coroutine to despawn the small projectile
            StartCoroutine(DestroyAfterTime(smallProjectile, despawnTime));
        }
    }

    Vector3 GetRandomDirection()
    {
        float theta = Random.Range(0f, Mathf.PI * 2);
        float phi = Random.Range(0f, Mathf.PI);
        
        float x = Mathf.Sin(phi) * Mathf.Cos(theta);
        float y = Mathf.Cos(phi);
        float z = Mathf.Sin(phi) * Mathf.Sin(theta);
        
        return new Vector3(x, y, z);
    }

    private IEnumerator DestroyAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }

    private IEnumerator ExplosionDelay(float explosionDelayTime)
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        hasExploded = true;
        yield return new WaitForSeconds(explosionDelayTime);
        Explode();
        rb.constraints = RigidbodyConstraints.None;
    }
}

