using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private Transform platform;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals((playerTag)))
        {
            other.gameObject.transform.parent = platform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals((playerTag)))
        {
            other.gameObject.transform.parent = null;
        }
    }
    
    
}
