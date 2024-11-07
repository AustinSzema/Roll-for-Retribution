using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSphere : MonoBehaviour
{
    [SerializeField] private ExplodingChargerAttack _explodingChargerAttack;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagManager.playerTag))
        {
            _explodingChargerAttack.Explode();
        }
    }
}
