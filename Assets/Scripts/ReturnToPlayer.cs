using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ReturnToPlayer : MonoBehaviour
{
    [SerializeField] private Vector3Variable _playerPos;

    [SerializeField] private Rigidbody _rigidbody;
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            _rigidbody.velocity = Vector3.zero;
            transform.position = _playerPos.Value + Vector3.up * 5f;
        }
    }
}
