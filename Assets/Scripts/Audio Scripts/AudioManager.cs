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
<<<<<<< Updated upstream
    
    public void AddClipToQueue(AudioClip _clip, Vector3 position)
=======
    private float objectReachedCooldown = 0.35f; // cooldown time in seconds for object reached sound
    private float lastObjectReachedTime = -1f; // last time the object reached sound was played

    public void AddClipToQueue(AudioClip clip, Vector3 position)
>>>>>>> Stashed changes
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


<<<<<<< Updated upstream
=======
    // stop the continuous pulling sound
    public void StopPullingSound()
    {
        _pullingSource.Stop();
        _pullingSource.loop = false;
    }

    // plays the sound when an object reaches the player at random intervals and pitches
    public void PlayObjectReachedSound()
    {
        if (Time.time - lastObjectReachedTime >= objectReachedCooldown)
        {
            _sfxSource.pitch = Random.Range(0.5f, 1.2f); 
            
            _sfxSource.PlayOneShot(_objectReachedClip);
            
            float randomCooldown = objectReachedCooldown * Random.Range(0.35f, 0.9f);
            lastObjectReachedTime = Time.time + randomCooldown - objectReachedCooldown; // ensures variability in interval timing

            _sfxSource.pitch = 1.0f;
        }
    }

    // for playing background music
    public void PlayMusic(AudioClip musicClip)
    {
        _musicSource.clip = musicClip;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    // for playing a sound effect
    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }
>>>>>>> Stashed changes
}
