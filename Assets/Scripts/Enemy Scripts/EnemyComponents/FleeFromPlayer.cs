using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeFromPlayer : EnemyComponent
{

    [SerializeField] private float rangeFromPlayer = 10f;

    private GameManager _gameManager;
    private bool fleeing = false;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void FixedUpdate()
    {
        if (!_gameManager.gameIsPaused && fleeing)
        {
            // Calculate the direction away from the player
            Vector3 directionAwayFromPlayer = (transform.position - _gameManager.playerPosition).normalized;
            
            // Move the enemy in the opposite direction of the player
            rb.position +=  enemyBase.enemySO._moveSpeed * Time.deltaTime * directionAwayFromPlayer;
        }
    }

    private void Update()
    {
        // Start fleeing if within a certain range from the player
        fleeing = Vector3.Distance(transform.position, _gameManager.playerPosition) < rangeFromPlayer;
    }
}