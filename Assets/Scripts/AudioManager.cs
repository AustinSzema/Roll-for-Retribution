using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    /*
    [SerializeField] private AudioClip _hitClip;
    [SerializeField] private AudioClip _bigHitClip;
    */


    public Queue<AudioClip> _queue = new Queue<AudioClip>();

    private Vector3 _lastPosition;
    
    public void AddClipToQueue(AudioClip _clip, Vector3 position)
    {
        if (Vector3.Distance(_lastPosition, position) > 1)
        {
            _queue.Enqueue(_clip);
            _lastPosition = position;
        }/*
        else
        {
            _queue.Enqueue(_bigHitClip);
            _lastPosition = position;
        }*/
    }


    private void Update()
    {
        if (_queue.Count > 0)
        {
            /*if (_queue.Dequeue().name == _bigHitClip.name)
            {
                _audioSource.pitch = 1f;
                _audioSource.PlayOneShot(_queue.Dequeue());
            }
            else
            {
                _audioSource.pitch = Random.Range(0.5f, 1.5f);
                _audioSource.PlayOneShot(_queue.Dequeue());
            }*/
            PlayHitSound();
        }
    }

    public void PlayHitSound()
    {
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_queue.Dequeue());
    }


}
