using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        // Get the direction from the enemy to the player
        Vector3 directionToPlayer = _gameManager.playerPosition - transform.position;
        directionToPlayer.y = 0f; // Ignore the y-axis

        // Check if the direction is non-zero before attempting rotation
        if (directionToPlayer != Vector3.zero)
        {
            // Calculate the desired rotation based on the direction
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            // Smoothly rotate the enemy towards the player only on the Y axis
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
