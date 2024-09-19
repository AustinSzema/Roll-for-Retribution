using UnityEngine;
using UnityEngine.Serialization;

public abstract class Magnetic : MonoBehaviour
{
    [SerializeField] protected float shootForce = 30.0f;
    [SerializeField] protected int damage = 1;

    [SerializeField] protected Rigidbody _rigidbody;

    protected virtual void Start()
    {
        _rigidbody.AddForce(Random.onUnitSphere * 100f); // Common initialization
    }

    
    private void OnCollisionEnter(Collision other)
    {
        HitEnemy(other.gameObject);
    }

    private void OnCollisionStay(Collision other)
    {
        HitEnemy(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        HitEnemy(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        HitEnemy(other.gameObject);
    }



    // Unified method to handle hitting an enemy
    protected void HitEnemy(GameObject target)
    {
        // Check if the target has IDamageable and is not the player
        if (target.TryGetComponent<IDamageable>(out IDamageable damageable) && !target.TryGetComponent<PlayerController>(out _))
        {
            Vector3 direction = (transform.position - target.transform.position).normalized;
            _rigidbody.AddForce(direction * shootForce, ForceMode.Impulse);
            damageable.takeDamage(damage);
        }
    }
}