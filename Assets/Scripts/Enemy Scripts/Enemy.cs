using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int healthPoints; 
    
    [SerializeField] private GameObject _enemy;

    [SerializeField] private intVariable _killCount;

    [SerializeField] private ParticleSystem _explosionParticles;

    private int _currentHealth;
    
    private void Awake()
    {
        // this needs to be refactored when proper health system is in place
        // health should not be hard coded in awake
        _currentHealth = healthPoints;
    }


    public void takeDamage(int hitPoints)
    {
        _currentHealth -= hitPoints;
        if (_currentHealth <= 0)
        {
            Explode();
            _killCount.Value++;
            _currentHealth = healthPoints;
        }
    }

    private void Explode()
    {
        _explosionParticles.Play();
        _enemy.SetActive(false);
        
    }

    private void Update()
    {
        if (transform.position.y < -5f)
        {
            takeDamage(1);
        }
    }
}
