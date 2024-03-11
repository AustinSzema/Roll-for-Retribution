using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource; // dedicated to background music
    [SerializeField] private AudioSource _sfxSource; // dedicated to sound effects
    [SerializeField] private AudioSource _pullingSource; // continuous pulling sound
    [SerializeField] private AudioSource _flyingSource; // continuous flying sound

    [SerializeField] private AudioClip _pullingClip;
    [SerializeField] private AudioClip _pullingEndClip;
    [SerializeField] private AudioClip _objectReachedClip; // Played when an object reaches the player
    [SerializeField] private AudioClip _shotgunClip;
    [SerializeField] private AudioClip _slamClip;
    [SerializeField] private AudioClip _flyingClip;
    [SerializeField] private AudioClip _flyStartClip;
    [SerializeField] private AudioClip _flyEndClip;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _hitClip;
    [SerializeField] private AudioClip _bigHitClip;


    private Queue<AudioClip> _queue = new Queue<AudioClip>();
    private Vector3 _lastPosition;
    private float objectReachedCooldown = 0.5f; // cooldown time in seconds for object reached sound
    private float lastObjectReachedTime = -1f; // last time the object reached sound was played

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
        /*
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_queue.Dequeue());
        */
    }

    // Will refactor the looping clips into one function when optimizing

    // start playing the continuous pulling sound
    public void StartPullingSound()
    {
        if (!_pullingSource.isPlaying)
        {
            _pullingSource.clip = _pullingClip;
            _pullingSource.loop = true;
            _pullingSource.Play();
        }
    }

    // stop the continuous pulling sound
    public void StopPullingSound()
    {
        _pullingSource.Stop();
        _pullingSource.loop = false;
    }

    // start playing the continuous flying sound
    public void StartFlyingSound()
    {
        _flyingSource.volume = 2.5f;
        if(!_flyingSource.isPlaying)
        {
            PlayFlyStartSound();
            _flyingSource.clip = _flyingClip;
            _flyingSource.loop = true;
            _flyingSource.Play();
        }
    }

    // stop the continuous flying sound
    public void StopFlyingSound()
    {
        if(_flyingSource.isPlaying)
        {
            _flyingSource.Stop();
            _flyingSource.loop = false;
            PlayFlyEndSound();
        }
    }

    // plays the sound when an object reaches the player at random intervals and pitches
    public void PlayObjectReachedSound()
    {
        if (Time.time - lastObjectReachedTime >= objectReachedCooldown)
        {
            if(_sfxSource.pitch > 0.8f && _sfxSource.pitch < 1.2f) {
                 _sfxSource.pitch *= Random.Range(0.5f, 1.5f);
            } else {
                _sfxSource.pitch = 1.0f;
            }
            
            _sfxSource.PlayOneShot(_objectReachedClip, 0.5f);
            
            float randomCooldown = objectReachedCooldown * Random.Range(0.5f, 1.5f);
            lastObjectReachedTime = Time.time + randomCooldown - objectReachedCooldown; // ensures variability in interval timing
        }
    }

    public void PlayShotgunSound()
    {
        _sfxSource.PlayOneShot(_shotgunClip);
    }

    public void PlaySlamSound()
    {
        _sfxSource.PlayOneShot(_slamClip);
    }

    public void PlayPullingEndSound()
    {
        _sfxSource.PlayOneShot(_pullingEndClip);
    }

    public void PlayFlyStartSound()
    {
        _sfxSource.PlayOneShot(_flyStartClip);
    }

    public void PlayFlyEndSound()
    {
        _sfxSource.PlayOneShot(_flyEndClip);
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
}
