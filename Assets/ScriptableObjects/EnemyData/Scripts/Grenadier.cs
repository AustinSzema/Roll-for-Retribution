using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenadier : Enemy
{
    [Header("Grenadier Specific Fields")]
    public Rigidbody grenade;

    private void Awake()
    {
        if (enemySO.enemyType != EnemySO.EnemyType.Grenadier)
        {
            throw new Exception("enemySO is not a Grenadier");
        }
    }

    protected override void Start()
    {
        Setup();
        StartCoroutine(Shoot());
    }
    
    protected virtual void FixedUpdate()
    {
        if (_gameManager.gameIsPaused && enemyShouldMove)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, _gameManager.playerPosition);

            // If the distance is greater than the minimum distance, move towards the player
            if (distanceToPlayer > enemySO.minDistanceFromPlayer)
            {
                rb.MovePosition(Vector3.MoveTowards(transform.position, _gameManager.playerPosition, enemySO._moveSpeed * Time.deltaTime));
            }
            // If the distance is less than or equal to the minimum, move away from the player
            else
            {
                // Calculate the direction away from the player
                Vector3 directionAwayFromPlayer = (transform.position - _gameManager.playerPosition).normalized;
                
                // Move in the opposite direction of the player
                rb.MovePosition(Vector3.MoveTowards(transform.position, transform.position + directionAwayFromPlayer, enemySO._moveSpeed * Time.deltaTime));   
            }
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(enemySO.projectileCooldown);

        // Activate the grenade
        grenade.gameObject.SetActive(true);

        grenade.velocity = Vector3.zero;
        grenade.transform.position = transform.position + Vector3.forward * 5f;
        // Get the direction to the player
        Vector3 directionToPlayer = (_gameManager.playerPosition - transform.position).normalized;

        // Set the grenade's velocity towards the player
        grenade.velocity = directionToPlayer * 100f;

        // Optionally, add some height to the grenade's velocity to simulate an arc
        grenade.velocity += Vector3.up * 30f;
    }

    
}