using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplosiveCrate : MonoBehaviour, IDamageable
{
    public int healthPoints { get; set;}

    [SerializeField] private Rigidbody rb;

    [SerializeField] private ParticleSystem explosionParticles;

    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionPower;

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
        explosionParticles.Play();

        //transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        //collider.enabled = false;
        //meshRenderer.enabled = false;


        Vector3 explosionPos = transform.position;


        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionPower, explosionPos, explosionRadius, 100f);
            }

        }


        gameObject.SetActive(false);

    }


}
