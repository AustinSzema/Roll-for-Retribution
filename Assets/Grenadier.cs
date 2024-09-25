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

    protected virtual void FixedUpdate()
    {
        if (_gameManager.gameIsPaused && enemyShouldMove)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, _gameManager.playerPosition);

            // Move towards the player only if the distance is greater than the minimum distance
            if (distanceToPlayer > minDistanceFromPlayer)
            {
                rb.MovePosition(Vector3.MoveTowards(transform.position, _gameManager.playerPosition, _moveSpeed * Time.deltaTime));
            }
        }
    }
}