using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource; // dedicated to background music
    [SerializeField] private AudioSource _sfxSource; // dedicated to sound effects
    [SerializeField] private AudioSource _invariableSFXSource; // dedicated to sound effects that should not be pitch changed
    [SerializeField] private AudioSource _pullingSource; // continuous pulling sound
    [SerializeField] private AudioSource _flyingSource; // continuous flying sound

    [SerializeField] private AudioClip _pullingClip;
    [SerializeField] private AudioClip _pullingEndClip;
    [SerializeField] private AudioClip _objectReachedClip; // Played when an object reaches the player
    [SerializeField] private AudioClip _shotgunClip;
    [SerializeField] private AudioClip _sniperClip;
    [SerializeField] private AudioClip _sprayClip;
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

    
    
    // Static reference to the GameManager instance
    private static AudioManager _instance;

    // Property to access the GameManager instance
    public static AudioManager Instance
    {
        get
        {
            // If instance hasn't been set yet, try to find it in the scene
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();

                // If it still hasn't been found, create a new GameObject and add GameManager to it
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("AudioManager");
                    _instance = singletonObject.AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }

    // Ensure AudioManager instance is not destroyed when loading new scenes
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists and it's not this instance, destroy this one
            if (this != _instance)
            {
                Destroy(gameObject);
            }
        }
    }
    
    /*public void AddClipToQueue(AudioClip _clip, Vector3 position)
    {
        if (Vector3.Distance(_lastPosition, position) > 1)
        {
            _queue.Enqueue(_clip);
            _lastPosition = position;
        }
        else
        {
            _queue.Enqueue(_bigHitClip);
            _lastPosition = position;
        }
    }*/

    
    /*public void PlayHitSound()
    {
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_queue.Dequeue());
    }*/

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

    public void PlaySniperSound()
    {
        _sfxSource.PlayOneShot(_sniperClip);
    }

    public void PlaySpraySound()
    {
        _sfxSource.PlayOneShot(_sprayClip);
    }

    public void PlayPullingEndSound()
    {
        _sfxSource.PlayOneShot(_pullingEndClip);
    }

    public void PlayFlyStartSound()
    {
        PlayInvariableSFX(_flyStartClip);
    }

    public void PlayFlyEndSound()
    {
        PlayInvariableSFX(_flyEndClip);
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

    public void PlayInvariableSFX(AudioClip clip)
    {
        _invariableSFXSource.PlayOneShot(clip);
    }

    public void PlaySFXAtLocation(AudioClip clip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(clip, position);
    }

    public void PlaySFXAtLocationWithVolume(AudioClip clip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlaySFXAtLocationWithPitch(AudioClip clip, Vector3 position, float pitch)
    {
        GameObject tempAudioObject = new GameObject("TempAudio");
        tempAudioObject.transform.position = position;

        AudioSource source = tempAudioObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.pitch = pitch;

        source.Play();
        GameObject.Destroy(tempAudioObject, clip.length); // instantiating and destroying objects this often is bad for performance
        // TODO: find an object pooled or more performant solution for audio
    }

}
