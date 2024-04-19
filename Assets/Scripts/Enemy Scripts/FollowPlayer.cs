using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;

    [SerializeField] private Vector3Variable _playerPosition;

    [SerializeField] private boolVariable _gameIsPaused;

    [SerializeField] private Rigidbody _rigidbody;
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_gameIsPaused.Value == false)
        {
            _rigidbody.position = Vector3.MoveTowards(transform.position, _playerPosition.Value, _moveSpeed * Time.deltaTime);
        }
    }
}


