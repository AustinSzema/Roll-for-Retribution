using UnityEngine;

public class GenericDamageEnemy : MonoBehaviour
{
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
        }
    }
    
    private void HitEnemy(Collider other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null && other.gameObject.GetComponent<PlayerController>() == null)
        {
            Debug.Log("Hit: " + other.gameObject.name);
            other.gameObject.GetComponent<IDamageable>().takeDamage(1);
        }
    } 
}
