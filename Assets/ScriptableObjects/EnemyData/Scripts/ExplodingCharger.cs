using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplodingCharger : Enemy
{
    private bool explosionHasBeenTriggered = false;


    [Header("Exploding Charger Specific Fields")]
    [SerializeField] private ParticleSystem explosionParticles;

    [SerializeField] private MeshRenderer meshRenderer;

    protected override void Update()
    {
        OnTick();
        ExplodeIfInRange();
        Debug.Log("explosion dmaage " + enemySO.explosionDamage);
    }

    private IEnumerator BeginExplosion()
    {
        if (explosionHasBeenTriggered)
        {
            yield return null;
        }
        explosionHasBeenTriggered = true;

        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(enemySO.explosionTimer);

        float particlesScale = enemySO.explosionDiameter / 15f; // magic number :D

        explosionParticles.transform.localScale = new Vector3(particlesScale, particlesScale, particlesScale);
        explosionParticles.Play();

        
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, enemySO.explosionDiameter / 2, transform.forward);
        foreach (RaycastHit hit in hits)
        {
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();

            Enemy possibleEnemy = hit.transform.GetComponent<Enemy>(); // TODO: optimize this

            if (damageable != null)
            {
                if (enemySO.damagesOtherEnemies)
                {
                    if (possibleEnemy != null)
                    {
                        Explode(damageable);
                    }
                }
                else
                {
                    if (possibleEnemy == null)
                    {
                        Explode(damageable);
                    }
                }
            }
        }
        gameObject.SetActive(false);
    }

    private void Explode(IDamageable damageable)
    {
        damageable.takeDamage(enemySO.explosionDamage);
    }

    private void ExplodeIfInRange()
    {
        Debug.Log("DistanceFromPlayer " + DistanceFromPlayer());
        if (!explosionHasBeenTriggered && DistanceFromPlayer() < enemySO.activationDistance)
        {
            StartCoroutine(BeginExplosion());
        }
    }

    private float DistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, GameManager.Instance.playerPosition);
    }

    private void OnEnable()
    {
        explosionHasBeenTriggered = false;
        meshRenderer.material.color = Color.blue;
    }

    private void OnDrawGizmos()
    {
        // Check if enemySO is assigned and valid
        if (enemySO == null) return;

        // Set the color for the gizmos (optional)
        Gizmos.color = Color.red;

        // Draw a sphere at the position of the enemy with a radius based on the explosion diameter
        Gizmos.DrawWireSphere(transform.position, enemySO.explosionDiameter / 2);
    }

}