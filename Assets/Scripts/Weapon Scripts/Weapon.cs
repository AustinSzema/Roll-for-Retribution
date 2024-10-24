using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float shootForce = 6000f;
    [SerializeField] protected float slamForce = 5500f;
    [SerializeField] protected float pullSpeed = 60f;

    public Rigidbody Rb { get; private set; }
    
    
    [SerializeField] public float damage = 1;

    [FormerlySerializedAs("_rigidbody")] [SerializeField] protected Rigidbody rb;
    public Sprite weaponUISprite;
    [Multiline]
    public string weaponDescription = "This is a weapon.";

    public string weaponName = "weapon";

    protected virtual void OnCollisionEnter(Collision other)
    {
        HitEnemy(other.gameObject);
    }

    protected virtual void OnCollisionStay(Collision other)
    {
        HitEnemy(other.gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        HitEnemy(other.gameObject);
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        HitEnemy(other.gameObject);
    }


    public virtual void Slam()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.down * slamForce);
    }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }


    public virtual void Shoot(Vector3 magnetForwardDirection) // public because magnet needs to access it
    {
        Vector3 offsetDirection;
        float deviationAngleX = 0f;
        float deviationAngleY = 0f;
        float deviationAngleZ = 0f;
        
        deviationAngleX = Random.Range(-20f, 20f);
        deviationAngleY = Random.Range(-20f, 20f);
        deviationAngleZ = Random.Range(-20f, 20f);
        offsetDirection = Quaternion.Euler(deviationAngleX, deviationAngleY, deviationAngleZ) *
                          magnetForwardDirection;
        rb.AddForce(offsetDirection * shootForce);
    }

    public virtual void Attract(Vector3 magnetPosition)
    {
        rb.velocity = Vector3.zero;
        rb.position = Vector3.MoveTowards(rb.position, magnetPosition,
            Time.deltaTime * pullSpeed);
    }

    public void SetRotation(Quaternion targetRotation)
    {
        // Apply the rotation to the spear and its Rigidbody
        transform.rotation = targetRotation;
    }
    
    
    // Unified method to handle hitting an enemy
    protected virtual void HitEnemy(GameObject target)
    {
        // Check if the target has IDamageable and is not the player
        if (target.TryGetComponent<IDamageable>(out IDamageable damageable) && !target.TryGetComponent<PlayerController>(out _))
        {
            //Vector3 direction = (transform.position - target.transform.position).normalized;
            //rb.AddForce(direction * shootForce, ForceMode.Impulse);
            damageable.takeDamage(damage);
        }
    }
}