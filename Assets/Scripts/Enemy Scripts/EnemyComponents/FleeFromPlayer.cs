using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeFromPlayer : EnemyComponent
{
    [SerializeField] private float fleeRangeFromPlayer = 10f;
    [SerializeField] private float fleeingSpeedMultiplier = 0f;
    //[SerializeField] private float fleeDuration = 0f;
    [SerializeField] private Rigidbody rb;

    private GameManager _gameManager;
    private bool fleeing = false;

    private void Start()
    {
        _gameManager = GameManager.Instance;

        // Null check for rb to ensure it’s assigned in the Inspector
        if (rb == null)
        {
            Debug.LogError("Rigidbody is not assigned on " + gameObject.name);
        }

        if (enemyBase == null || enemyBase.enemySO == null)
        {
            Debug.LogError("EnemyBase or enemySO is not assigned on " + gameObject.name);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(TagManager.groundTag))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (!_gameManager.gameIsPaused && fleeing)
        {
            // Calculate the direction away from the player
            Vector3 directionAwayFromPlayer = (transform.position - _gameManager.playerPosition).normalized;

            // Move the enemy in the opposite direction of the player
            float moveSpeed = enemyBase.enemySO._moveSpeed;
            //Vector3 newPosition = rb.position + moveSpeed * Time.fixedDeltaTime * directionAwayFromPlayer;
            //rb.MovePosition(newPosition);
            rb.AddForce(directionAwayFromPlayer * (moveSpeed * fleeingSpeedMultiplier), ForceMode.Acceleration);
        }
    }

    private void Update()
    {
        // Start fleeing if within a certain range from the player
        //fleeing = Vector3.Distance(transform.position, _gameManager.playerPosition) < rangeFromPlayer;
        if (Vector3.Distance(transform.position, _gameManager.playerPosition) < fleeRangeFromPlayer)
        {
            fleeing = true;
        }
        else
        {
            fleeing = false; 
        }
        //Debug.Log(fleeing);
        //Debug.Log(Vector3.Distance(transform.position, _gameManager.playerPosition));
    }
    
    /*IEnumerator FleeCooldown()
    {
        yield return new WaitForSeconds(fleeDuration);
        Debug.Log("Cooldown active");
    }*/
}