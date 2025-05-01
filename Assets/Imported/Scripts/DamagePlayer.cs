using System;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Enemy sliced " + other.gameObject.name);
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
           
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemy.damage);
            }
            else
            {
                Debug.LogError("PlayerHealth script not found on Player!");
            }
            
        }
    }
}
