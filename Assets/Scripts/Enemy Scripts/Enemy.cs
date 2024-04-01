using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int healthPoints = 1; 
    
    [SerializeField] private GameObject _enemy;

    [SerializeField] private intVariable _killCount;

    [SerializeField] private ParticleSystem _explosionParticles;

    [SerializeField] private AudioClip _hitClip;

    private static AudioManager _audioManager;

    private int _currentHealth;

    private bool _firstDisable = true;
    
    private void Awake()
    {
        // this needs to be refactored when proper health system is in place
        // health should not be hard coded in awake
        _currentHealth = healthPoints;
        _audioManager = FindObjectOfType<AudioManager>();

        if (_hitClip == null)
        {
            _hitClip = Resources.Load<AudioClip>("Audio/Hit");
        }
    }

    private void OnDisable()
    {
        if (_firstDisable)
        {
            _firstDisable = false;
        }
        else
        {
            SpawnEnemies.EnemiesInScene--;
            _killCount.Value++;
        }
    }


    public void takeDamage(int hitPoints)
    {
        _currentHealth -= hitPoints;
        if (_currentHealth <= 0)
        {
            Explode();
            _currentHealth = healthPoints;
        }
    }

    private void Explode()
    {
        if (_hitClip != null)
        {
            _audioManager.PlaySFXAtLocation(_hitClip, transform.position);
        }
        else
        {
            Debug.LogWarning("Hit clip is null in " + gameObject.name);
        }
        _explosionParticles.Play();
        _enemy.SetActive(false);
    }

    private void Update()
    {
        if (transform.position.y <=-5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        }
    }
}
