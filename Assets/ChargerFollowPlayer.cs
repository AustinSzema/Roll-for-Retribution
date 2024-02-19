using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerFollowPlayer : MonoBehaviour
{    
    [SerializeField] private float _moveSpeed = 2f;

    [SerializeField] private Vector3Variable _playerPosition;

    [SerializeField] private boolVariable _gameIsPaused;

    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private int moveInterval;

    [SerializeField] private float targetDistanceBehindPlayer;

    private bool _charging;

    private Vector3 goalPosition;

    private void Awake()
    {
      _charging = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_gameIsPaused.Value == false)
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
      float distToPlayer = Vector3.Distance(transform.position, _playerPosition.Value);
      Vector3 moveDirection = (_playerPosition.Value - transform.position).normalized;
      goalPosition = moveDirection * (distToPlayer + targetDistanceBehindPlayer);
      yield return new WaitForSeconds(moveInterval);
      _charging = false;
    }
}
