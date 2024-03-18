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

    private Vector3 _movement;
    private bool _charging;


    private void Awake()
    {
      _charging = false;
    }


    private void FixedUpdate()
    {
      if (_gameIsPaused.Value == false && this.gameObject.activeSelf)
      {
        if (_charging == false)
        {
          StartCoroutine(Charge());
        }
        else
        {
          //transform.position = Vector3.MoveTowards(transform.position, goalPosition, _moveSpeed * Time.deltaTime);
          _rigidbody.AddForce(_movement, ForceMode.Acceleration);
        }
      }

    }

    IEnumerator Charge()
    {
      _charging = true;
      
      float distToPlayer = Vector3.Distance(transform.position, _playerPosition.Value);
      Vector3 moveDirection = (_playerPosition.Value - transform.position).normalized;
      _movement = moveDirection * _moveSpeed;
      yield return new WaitForSeconds(moveInterval);
      _rigidbody.velocity = Vector3.zero;
      _charging = false;
    }
    
    
}
