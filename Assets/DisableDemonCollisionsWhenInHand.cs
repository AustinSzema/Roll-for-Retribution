using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDemonCollisionsWhenInHand : MonoBehaviour
{
    [SerializeField] private Vector3Variable _handPos;

    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private LayerMask _demonLayer;
    
    [SerializeField] private LayerMask _nothing;
    // void Update()
    // {
    //     if (Vector3.Distance(transform.position, _handPos.Value) < 5f)
    //     {
    //         _rigidbody.excludeLayers = _demonLayer;
    //     }
    //     else
    //     {
    //         _rigidbody.excludeLayers = _nothing;
    //     }
    //     
    // }
}
