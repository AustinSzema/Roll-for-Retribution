using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperFollowPlayer : MonoBehaviour
{    
    [SerializeField] private float _moveSpeed = 2f;
    
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private int moveInterval;

    [SerializeField] private float targetDistanceBehindPlayer;

    private bool _charging;

    private Vector3 goalPosition;

    private GameManager _gameManager;
    
    private void Awake()
    {
        _charging = false;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_gameManager.gameIsPaused == false)
        {
            if (_charging == false)
            {
                StartCoroutine(Charge());
            }
            else
            {
                _rigidbody.position = Vector3.MoveTowards(transform.position, goalPosition, _moveSpeed * Time.deltaTime);
            }
        }
    }
    IEnumerator Charge()
    {
        _charging = true;
      
        float distToPlayer = Vector3.Distance(transform.position, _gameManager.playerPosition);
        Vector3 moveDirection = (_gameManager.playerPosition - transform.position).normalized;
        goalPosition = transform.position + moveDirection * (distToPlayer + targetDistanceBehindPlayer);
        goalPosition.y = _gameManager.playerPosition.y;
        yield return new WaitForSeconds(moveInterval);
        _charging = false;
    }
}
