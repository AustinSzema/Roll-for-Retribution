using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadierGasBomb : MonoBehaviour
{
    private float scale = 50f;
    private Vector3 expandSize = new Vector3(10f, 10f, 10f);
    [SerializeField] private Rigidbody rb;
    
    private void Start()
    {
        expandSize = new Vector3(scale, scale, scale);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(TagManager.enemyTag))
        {
            rb.velocity = Vector3.zero;
            transform.localScale = expandSize;
            StartCoroutine(Shrink());
        }
    }

    private IEnumerator Shrink()
    {
        while (transform.localScale.magnitude > 1f)
        {
            transform.localScale *= 0.9999f; 
            yield return null;
        }
    }
}
