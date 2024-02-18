using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour, IDamageable
{
    public int healthPoints { get; set; }
    
    [SerializeField] private GameObject _enemy;

    [SerializeField] private intVariable _killCount;

    [SerializeField] private ParticleSystem _explosionParticles;
    
    private void Awake()
    {
        // this needs to be refactored when proper health system is in place
        // health should not be hard coded in awake
        healthPoints = 1;
    }


    public void takeDamage(int hitPoints)
    {
        healthPoints -= hitPoints;
        if (healthPoints <= 0)
        {
            Explode();
            _killCount.Value++;
            healthPoints = 1;
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
