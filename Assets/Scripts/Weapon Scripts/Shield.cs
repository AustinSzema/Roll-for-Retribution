using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shield : MonoBehaviour
{

    [SerializeField] private ParticleSystem _shieldParticles;
    [SerializeField] private Transform _shieldParticlesTransform;
    
    [SerializeField] private float moveSpeed = 20f;

    private int shieldHealth = 10;
    private int _enemyHitCount = 0;

    private float shieldDuration = 0.8f;

    private Vector3 startRotation = Vector3.forward;

    [SerializeField] private Rigidbody rb;

    private void Start()
    { 
        startRotation = transform.forward;
        rb.velocity = transform.forward * 100f;
        StartCoroutine(RotateCard(0.005f));
        StartCoroutine(RemoveShield(shieldDuration));
    }

    private IEnumerator RotateCard(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.AddTorque(500f * NegOnePosOne() * transform.right);
        rb.AddTorque(100f * NegOnePosOne() * transform.up);
    }

    private int NegOnePosOne() // returns -1 or 1
    {
        return Random.Range(0, 1) * 2 - 1;
    }

    // private void Update()
    // {
    //     transform.position = Vector3.MoveTowards(transform.position, startRotation * 100f, Time.deltaTime);
    // }

    private IEnumerator RemoveShield(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        DestroyShield();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        HitEnemy(other);
    }

    private void OnCollisionStay(Collision other)
    {
        HitEnemy(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        HitEnemy(other);
    }
    
    private void OnTriggerStay(Collider other)
    {
        HitEnemy(other);
    }

    private void HitEnemy(Collision other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null && other.gameObject.GetComponent<PlayerController>() == null)
        {
            other.gameObject.GetComponent<IDamageable>().takeDamage(1);
            _enemyHitCount++;
            if (_enemyHitCount >= shieldHealth)
            {
                DestroyShield();
            }
        }
    }
    
    private void HitEnemy(Collider other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null && other.gameObject.GetComponent<PlayerController>() == null)
        {
            other.gameObject.GetComponent<IDamageable>().takeDamage(1);
            _enemyHitCount++;
            if (_enemyHitCount >= shieldHealth)
            {
                DestroyShield();
            }
        }
    }

    private void DestroyShield()
    {
        _shieldParticlesTransform.parent = null;
        _shieldParticles.Play();
        gameObject.SetActive(false);
    }
}
