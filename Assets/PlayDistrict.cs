using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDistrict : MonoBehaviour
{
    [SerializeField]
    private DistrictAudio districtAudio;

    [field: SerializeField] public AudioSource audioSource { get; private set; }

    [HideInInspector] public bool getLoud = false;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.playerTag))
        {
            districtAudio.SilenceAll();
            getLoud = true; // Start getting loud immediately
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagManager.playerTag))
        {
            getLoud = false; // Stop increasing volume when player leaves
            districtAudio.SilenceAll(); // Optional: could fade this one out too
        }
    }

    private void Update()
    {
        if (getLoud)
        {
            audioSource.volume = Mathf.Min(audioSource.volume + Time.deltaTime, 1f);
        }
        else
        {
            audioSource.volume = Mathf.Max(audioSource.volume - Time.deltaTime, 0f);
        }
    }

}
