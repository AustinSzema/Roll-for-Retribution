using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour, IDamageable
{
    public int healthPoints { get; set; }

    [SerializeField] private intVariable _bossHealth;
    
    private void Awake()
    {
        _bossHealth.Value = 100;
    }

    public void takeDamage(int hitPoints)
    {
        _bossHealth.Value -= hitPoints;
    }

    private void Update()
    {
        if (_bossHealth.Value <= 0)
        {
            Destroy(gameObject);
        }
    }
}
