using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shield : MonoBehaviour
{

    [SerializeField] private ParticleSystem _shieldParticles;
    [SerializeField] private Transform _shieldParticlesTransform;
    
    [SerializeField] private float moveSpeed = 20f;

    [SerializeField] private int shieldHealth = 200;
    private int _enemyHitCount = 0;

    [SerializeField] private float shieldDuration = 5f;

    private Vector3 startRotation = Vector3.forward;

    private void Start()
    { 
        startRotation = transform.forward;
        StartCoroutine(RemoveShield(shieldDuration));
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, startRotation * 100f, Time.deltaTime);
    }

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
            EZDebug.Log("Hit: " + other.gameObject.name);
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
