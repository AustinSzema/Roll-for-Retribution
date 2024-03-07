using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ReturnToPlayer : MonoBehaviour
{
    // Recalls all of the demon dice to the player when the player presses 'e'.
    // Does not fully work properly at the moment with low frame rates
    // If you shoot the demons really far away it takes multiple presses to get them to come back. Probably because their velocity is so high
    // Mainly for testing purposes, likely won't be in the final build
    
    [SerializeField] private Vector3Variable _playerPos;

    [SerializeField] private Rigidbody _rigidbody;
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            _rigidbody.velocity = Vector3.zero;
            transform.position = _playerPos.Value + Vector3.up * 5f;
            //Debug.Log(" press e on update");
        }
        
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _rigidbody.velocity = Vector3.zero;
            transform.position = _playerPos.Value + Vector3.up * 5f;
            //Debug.Log(" press e on fixedupdate");
        }    
    }
}
