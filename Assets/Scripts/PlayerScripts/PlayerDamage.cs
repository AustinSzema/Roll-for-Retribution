using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour, IDamageable
{
    public int healthPoints { get; set; }

    [SerializeField] private intVariable _playerHealth;
    private void Start()
    {
        _playerHealth.Value = 10;
        healthPoints = _playerHealth.Value;
    }



    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.GetComponent<Cow>() != null)
        {
            takeDamage(1);
        }
    }

    public void takeDamage(int hitPoints)
    {
        healthPoints-= hitPoints;
        _playerHealth.Value -= hitPoints;
        Debug.Log("player took 1 damage");
    }
}
