using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingCharger : Enemy
{
    
    [Header("ExplodingCharger Specific Values")]
    [SerializeField] private float explosionDamage = 25f;
    [SerializeField] private float explosionTimer = 3f;
    [SerializeField] private float explosionDiameter = 5f;
    [SerializeField] private float activationDistance = 15f;
    [SerializeField] private bool damagesOtherEnemies = true;

    private bool explosionHasBeenTriggered = false;


    [SerializeField] private ParticleSystem explosionParticles;


    protected override void Update()
    {
        OnTick();
        ExplodeIfInRange();
        Debug.Log("explosion dmaage " + enemySO.explosionDamage);
    }

    private IEnumerator BeginExplosion()
    {
        yield return new WaitForSeconds(explosionTimer);
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, explosionDiameter / 2, transform.forward);
        foreach (RaycastHit hit in hits)
        {
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();

            Enemy possibleEnemy = hit.transform.GetComponent<Enemy>(); // TODO: optimize this
            
            if (damageable != null)
            {
                if (damagesOtherEnemies)
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
    }

    private void Explode(IDamageable damageable)
    {
        damageable.takeDamage(explosionDamage);   
        explosionParticles.Play();
        gameObject.SetActive(false);
    }
    
    private void ExplodeIfInRange()
    {
        if (!explosionHasBeenTriggered && DistanceFromPlayer() < activationDistance)
        {
            StartCoroutine(BeginExplosion());
            explosionHasBeenTriggered = true;
        }
    }

    private float DistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, GameManager.Instance.playerPosition);
    }
    
    
}
