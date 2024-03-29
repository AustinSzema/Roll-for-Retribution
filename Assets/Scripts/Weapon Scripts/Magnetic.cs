using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Magnetic : MonoBehaviour
{
    private static AudioManager _audioManager;
    
    [SerializeField] private AudioClip _hitClip;
    [SerializeField] private ParticleSystem _dustParticles;
    [SerializeField] private float forceAmount = 30.0f;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        HitEnemy(other);
    }

    private void OnCollisionStay(Collision other)
    {
        HitEnemy(other);
    }

    private void HitEnemy(Collision other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null && other.gameObject.GetComponent<PlayerController>() == null)
        {
            Vector3 dir = (transform.position - other.gameObject.transform.position).normalized;
            _rigidbody.AddForce(dir * forceAmount, ForceMode.Impulse);
            //Debug.Log(other.gameObject.name);
            other.gameObject.GetComponent<IDamageable>().takeDamage(1);
            //_dustParticles.transform.position = other.contacts[0].point;
            //_dustParticles.Play();
            _audioManager.AddClipToQueue(_hitClip, transform.position);
        }
    }

    private void Update()
    {
        if (transform.position.y < 0f)
        {
            transform.position = new Vector3(transform.position.x, 30f, transform.position.z);
        }
    }
    
    
}


