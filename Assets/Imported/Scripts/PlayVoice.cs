using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayVoice : MonoBehaviour
{
    [SerializeField] private Voice voiceSO;
    [SerializeField] private UnityEvent voice_Transition;
    [SerializeField] private float voiceDelay = 10f;
    [SerializeField] private bool isVoiceTransitionScene;
    int voiceIndex = 0;

    [SerializeField] private AudioSource _audioSource;



    // Start is called before the first frame update
    void Start()
    {
        _audioSource.PlayOneShot(voiceSO.VoiceActing[voiceIndex]);
        InvokeRepeating("nextAudioClip", 10.0f, voiceDelay);
    }

    void nextAudioClip()
    {
        if (voiceIndex <= voiceSO.VoiceActing.Length - 1)
        {
            voiceIndex++;

            _audioSource.PlayOneShot(voiceSO.VoiceActing[voiceIndex]);

        }






    }





}




