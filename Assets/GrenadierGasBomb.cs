using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrenadierGasBomb : MonoBehaviour
{
    private float scale = 50f;
    [SerializeField] private Vector3 expandSize = new Vector3(10f, 10f, 10f);
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int shrinkDelay;
    [SerializeField] private float shrnkSpeedMultiplier;

    private void Start()
    {
        expandSize = new Vector3(scale, scale, scale);
        rb.velocity = Vector3.zero;
        transform.position = transform.position + Vector3.forward * 5f;
        Vector3 directionToPlayer = (GameManager.Instance.playerPosition - transform.position).normalized;
        rb.velocity = directionToPlayer * 100f;
        rb.velocity += Vector3.up * 20f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(TagManager.enemyTag))
        {
            rb.velocity = Vector3.zero;
            transform.localScale = expandSize;
            rb.isKinematic = true;
            StartCoroutine(WaitAndShrink());
        }
    }

    private IEnumerator Shrink()
    {
        while (transform.localScale.x > 0.1f)
        {
            if (!GameManager.Instance.gameIsPaused)
            {
                transform.localScale *= shrnkSpeedMultiplier;
                yield return new WaitForSeconds(0f);
            }
            else
            {
                yield return null;
            }
        }
        Destroy(gameObject);
    }
    private IEnumerator WaitAndShrink()
    {
        yield return new WaitForSeconds(shrinkDelay); 
        StartCoroutine(Shrink());
    }
    
}