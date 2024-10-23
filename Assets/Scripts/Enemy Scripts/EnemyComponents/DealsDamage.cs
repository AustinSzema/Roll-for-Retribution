using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealsDamage : EnemyComponent
{
    public float damageToDeal = 1f;

    [SerializeField] private bool dealsDamageOnStay = false;
    
    private void DealDamage(GameObject obj)
    {
        Debug.Log("hit this thing with this tag " + obj.name + " "  + obj.tag);
        if (obj.CompareTag(TagManager.playerTag))
        {
            PlayerDamage player = obj.GetComponent<PlayerDamage>();
            player.takeDamage(damageToDeal);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        DealDamage(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        DealDamage(other.gameObject);

    }

    private void OnCollisionStay(Collision other)
    {
        if (dealsDamageOnStay)
        {
            DealDamage(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (dealsDamageOnStay)
        {
            DealDamage(other.gameObject);
        }
    }


}
