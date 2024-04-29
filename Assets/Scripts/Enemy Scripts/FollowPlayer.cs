using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;

    [SerializeField] private Rigidbody _rigidbody;


    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_gameManager.gameIsPaused == false)
        {
            _rigidbody.position = Vector3.MoveTowards(transform.position, _gameManager.playerPosition, _moveSpeed * Time.deltaTime);
        }
    }
}


