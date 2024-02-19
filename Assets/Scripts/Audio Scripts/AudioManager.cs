using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource; // dedicated to background music
    [SerializeField] private AudioSource _sfxSource; // dedicated to sound effects
    [SerializeField] private AudioSource _pullingSource; // continuous pulling sound

    [SerializeField] private AudioClip _pullingClip;
    [SerializeField] private AudioClip _objectReachedClip; // Played when an object reaches the player

    private Queue<AudioClip> _queue = new Queue<AudioClip>();
    private Vector3 _lastPosition;
    private float objectReachedCooldown = 0.5f; // cooldown time in seconds for object reached sound
    private float lastObjectReachedTime = -1f; // last time the object reached sound was played

    public void AddClipToQueue(AudioClip clip, Vector3 position)
    {
        if (Vector3.Distance(_lastPosition, position) > 1)
        {
            _queue.Enqueue(clip);
            _lastPosition = position;
        }
    }

    private void Update()
    {
        if (_queue.Count > 0)
        {
            PlayHitSound();
        }
    }

    public void PlayHitSound()
    {
        _sfxSource.pitch = Random.Range(0.8f, 1.2f);
        _sfxSource.PlayOneShot(_queue.Dequeue());
    }

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

    // plays the sound when an object reaches the player at random intervals and pitches
    public void PlayObjectReachedSound()
    {
        if (Time.time - lastObjectReachedTime >= objectReachedCooldown)
        {
            _sfxSource.pitch = Random.Range(0.1f, 2.2f); 
            
            _sfxSource.PlayOneShot(_objectReachedClip);
            
            float randomCooldown = objectReachedCooldown * Random.Range(0.5f, 1.5f);
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
}
