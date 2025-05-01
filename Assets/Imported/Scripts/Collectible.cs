using System;
using UnityEngine;

public class Collectible : MonoBehaviour, IInteractable
{
    [SerializeField] private ParticleSystem collectionParticles;
    [SerializeField] private AudioClip pickupClip;
    public void Interact()
    {
       if (pickupClip != null)
        {
            //Debug.Log("Playing pickup sound!");

            // Create a temporary GameObject to play the sound
            GameObject tempAudio = new GameObject("TempPickupSound");
            AudioSource tempSource = tempAudio.AddComponent<AudioSource>();

            tempSource.clip = pickupClip;
            tempSource.spatialBlend = 0f; // Make it 2D so it plays anywhere
            tempSource.Play();

            Destroy(tempAudio, pickupClip.length);
        }
        else
        {
            Debug.LogWarning("Missing pickupClip!");
        }
        
        collectionParticles.Play();
        collectionParticles.transform.parent = null;
        gameObject.SetActive(false);
    }
    
}
