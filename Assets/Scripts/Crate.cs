using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, IDamageable
{
    public int healthPoints { get; set;}
    
    [SerializeField] private ParticleSystem explosionParticles;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Collider collider;


/*
 * 
 * 
    private void OnCollisionEnter(Collision collision)
    {
        takeDamage(healthPoints, 1f);
    }

    private void OnCollisionStay(Collision collision)
    {
        takeDamage(healthPoints, 1f);
    }*/

    public void takeDamage(int hitPoints)
    {
        healthPoints -= hitPoints;
        if (healthPoints <= 0f)
        {
            Explode();
        }
    }

    void Explode()
    {
        Debug.Log("Exploded");
        explosionParticles.Play();

        //transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        //collider.enabled = false;
        //meshRenderer.enabled = false;


/*        Vector3 explosionPos = transform.position;

        float radius = 20f;
        float power = 500f;

        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius);
            }

        }*/


        gameObject.SetActive(false);

    }


}
