using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade_Old : MonoBehaviour
{
    [SerializeField] GameObject smallProjectilePrefab;
    [SerializeField] private int numberofSchrapnelObjects = 10;
    [SerializeField] private float explosionForce = 10f; 
    [SerializeField] private float despawnTime = 3f; 
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            Explode();
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
            Rigidbody rb = smallProjectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Generate a random direction for each small projectile
                Vector3 randomDirection = GetRandomDirection();
                rb.AddForce(randomDirection * explosionForce, ForceMode.Impulse);
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
}


