using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerIsGrounded : MonoBehaviour
{

    // this might not work perfectly because the ground is bumpy
    // The player might think they're grounded when they're slightly off the ground
    // Idk if this will because visual bugs but if it does then we will replace collision checking with raycasting
    
    [SerializeField] private boolVariable _playerIsGrounded;

    private void OnCollisionEnter(Collision other)
    {
        SetIsGrounded(other, true);
    }

    private void OnCollisionStay(Collision other)
    {
        SetIsGrounded(other, true);
    }

    private void OnCollisionExit(Collision other)
    {
        SetIsGrounded(other, false);
    }

    private void SetIsGrounded(Collision other, bool value)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _playerIsGrounded.Value = value;
        }
    }
    
}
