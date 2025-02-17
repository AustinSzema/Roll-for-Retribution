using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingChargerAttack : MonoBehaviour
{
    [SerializeField] private EnemyBase enemyBase;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private ParticleSystem chargerExplosionParticles;
    [SerializeField] private Collider chargerExplosionCollider;

    [SerializeField] private TakesDamage takeDamage;
    
    private Vector3 playerPosition = Vector3.zero;
    private Vector3 chargerVelocity = Vector3.zero;
    private bool isCharging = false;

    private void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(Charge());
        }
    }

    private IEnumerator Charge()
    {
        // Wait before charging starts
        yield return new WaitForSeconds(1f);
        
        // Get the player's position and set the charging velocity
        playerPosition = GameManager.Instance.playerPosition;
        Vector3 direction = (playerPosition - transform.position).normalized;
        float chargeSpeed = 10f; // You can adjust this speed value
        chargerVelocity = direction * chargeSpeed;
        isCharging = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        // If the charger hits anything other than the player or another enemy, reset charge
        if (!other.gameObject.CompareTag(TagManager.playerTag) && !other.gameObject.CompareTag(TagManager.enemyTag))
        {
            chargerVelocity = Vector3.zero;
            isCharging = false;

            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(Charge());
            }
        }
    }

    
    public void Explode()
    {
        chargerVelocity = Vector3.zero;
        
        chargerExplosionParticles.Clear();
        chargerExplosionParticles.Play();
        chargerExplosionCollider.enabled = true;

        takeDamage.takeDamage(Int32.MaxValue);
    }
    
    private IEnumerator ExplodeCharger()
    {
        
        // Stop charging and play explosion particles
        isCharging = false;
        chargerVelocity = Vector3.zero;
        
        yield return new WaitForSeconds(0.2f);
        chargerExplosionParticles.Play();
        chargerExplosionCollider.enabled = true;
        
        // Disable the explosion collider after a short time
        yield return new WaitForSeconds(0.5f);
        chargerExplosionCollider.enabled = false;
    }

    private void Update()
    {
        // Rotate towards the player and apply velocity if charging
        if (isCharging)
        {
            Vector3 directionToPlayer = (GameManager.Instance.playerPosition - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            
            rb.linearVelocity = chargerVelocity;
        }
    }
}
