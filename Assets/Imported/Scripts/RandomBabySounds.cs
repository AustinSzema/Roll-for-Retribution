using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBabySounds : MonoBehaviour
{
    [SerializeField] private SoundArray _babySounds;
    
    private float _minDelay = 5.0f;
    private float _maxDelay = 10.0f;

    [SerializeField] private AudioSource _audioSource;
    private float _nextSoundTime;
    
    private int _lastSoundIndex = -1; // Initialize to -1 to ensure the first sound is different.

    void Start()
    {
        _nextSoundTime = Time.time + Random.Range(_minDelay, _maxDelay);
    }

    void Update()
    {
        if (Time.time >= _nextSoundTime)
        {
            int index;
            do
            {
                index = Random.Range(0, _babySounds._sounds.Length);
            } while (index == _lastSoundIndex); // Ensure the new index is different from the previous one.

            _audioSource.PlayOneShot(_babySounds._sounds[index]);
            _lastSoundIndex = index; // Update the last sound index.
            _nextSoundTime = Time.time + Random.Range(_minDelay, _maxDelay);
        }
    }
}
