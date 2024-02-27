using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDemonInHand : MonoBehaviour
{
    [SerializeField] private boolVariable _demonInHand;

    private void OnTriggerEnter(Collider other)
    {
        _demonInHand.Value = true;
    }

    private void OnTriggerStay(Collider other)
    {
        _demonInHand.Value = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _demonInHand.Value = false;
    }
}
