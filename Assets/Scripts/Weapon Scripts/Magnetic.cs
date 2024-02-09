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

    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null && other.gameObject.GetComponent<PlayerController>() == null)
        {
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


