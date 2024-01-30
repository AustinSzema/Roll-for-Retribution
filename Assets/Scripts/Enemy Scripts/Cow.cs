using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour, IDamageable
{
    public int healthPoints { get; set; }

    [SerializeField] private ParticleSystem _explosionParticles;

    [SerializeField] private GameObject _cowVisual;

    [SerializeField] private intVariable _killCount;
    
    private void Awake()
    {
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
        Debug.Log("played cow particle effects");
        _cowVisual.SetActive(false);
        
    }

    private void Update()
    {
        if (transform.position.y < -5f)
        {
            takeDamage(1);
        }
    }
}
