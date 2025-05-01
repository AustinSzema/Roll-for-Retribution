using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVoicelinePlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _voiceoverAudio;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private float _firstWaitTime = 5.0f;
    [SerializeField] private float _minTimeBetweenClips = 30.0f;
    [SerializeField] private float _maxTimeBetweenCips = 35.0f;

    private void Start()
    {
        Invoke("PlayRandomVoiceLine",_firstWaitTime);
    }

    private void PlayRandomVoiceLine()
    {
        int audioClipIndex = Random.Range(0, _clips.Length);
        _voiceoverAudio.PlayOneShot(_clips[audioClipIndex]);
        float waitTime = Random.Range(_minTimeBetweenClips, _maxTimeBetweenCips);
        Invoke("PlayRandomVoiceLine",waitTime);
    }
}
