using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerFollowPlayer : MonoBehaviour
{    
    [SerializeField] private float _moveSpeed = 2f;
    
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private int moveInterval = 3;

    private Vector3 _movement;
    private bool _charging;

    private GameManager _gameManager;

    private void Awake()
    {
      _charging = false;
    }

    private void Start()
    {
      _gameManager = GameManager.Instance;
    }


    private void FixedUpdate()
    {
      if (_gameManager.gameIsPaused == false && this.gameObject.activeSelf)
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
      
      Vector3 moveDirection = (_gameManager.playerPosition - transform.position).normalized;
      _movement = moveDirection * _moveSpeed;
      yield return new WaitForSeconds(moveInterval);
      _rigidbody.linearVelocity = Vector3.zero;
      _charging = false;
    }
    
    
}
