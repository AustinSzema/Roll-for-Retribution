using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : EnemyComponent
{
    private GameManager _gameManager;

    [SerializeField] private Renderer renderer;
    
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_gameManager.gameIsPaused == false && renderer.isVisible)
        {
            rb.position = Vector3.MoveTowards(transform.position, _gameManager.playerPosition, enemyBase.enemySO._moveSpeed * Time.deltaTime);
        }
    }
}


