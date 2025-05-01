using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWalkSound : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] 
    private AudioSource audioSource;
    // Update is called once per frame
    void Update()
    {
     if(rb.linearVelocity.x > 0f || rb.linearVelocity.z > 0f || rb.linearVelocity.x < 0f || rb.linearVelocity.z < 0f)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }   
    }
}
