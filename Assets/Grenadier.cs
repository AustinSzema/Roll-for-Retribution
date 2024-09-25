using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenadier : Enemy
{
    public float gasDamage = 5f;
    public float gasDuration = 10f;
    public float gasCloudDiameter = 15f;
    public bool damageOtherEnemies = true;
    public float projectileSpeed = 50f;
    public float projectileCooldown = 10f;
    public float idealDistanceToPlayer = 100f;
    [Tooltip("The radius around the player which the Grenadier will not enter")]
    public float minDistanceFromPlayer = 10f; // Minimum distance to maintain from the player

    public GameObject grenade;
    
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
            if (distanceToPlayer > minDistanceFromPlayer)
            {
                rb.MovePosition(Vector3.MoveTowards(transform.position, _gameManager.playerPosition, _moveSpeed * Time.deltaTime));
            }
            // If the distance is less than or equal to the minimum, move away from the player
            else
            {
                // Calculate the direction away from the player
                Vector3 directionAwayFromPlayer = (transform.position - _gameManager.playerPosition).normalized;
                
                // Move in the opposite direction of the player
                rb.MovePosition(Vector3.MoveTowards(transform.position, transform.position + directionAwayFromPlayer, _moveSpeed * Time.deltaTime));   
            }
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(projectileCooldown);
        Instantiate(grenade, transform.position, Quaternion.identity);
    }
    
}