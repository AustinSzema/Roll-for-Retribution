using UnityEngine;
using System.Collections;

public class RadioController : MonoBehaviour
{
    public GrabDetection grabDetection;
    private AudioSource radioAudioSource;

    [Header("Radio Clips")]
    public AudioClip[] radioClips = new AudioClip[4];
    public AudioClip radioStaticSound;
    public float stationChangeInterval = 300f; // 5 minutes in seconds

    private int currentClipIndex = 0;
    private bool isReadyForNextClip = false;
    private bool hasPlayedStatic = false;
    private bool isCurrentlyGrabbed = false;
    private bool wasGrabbedLastFrame = false;

    void Start()
    {
        if (grabDetection == null)
        {
            grabDetection = GetComponent<GrabDetection>();
        }

        radioAudioSource = gameObject.AddComponent<AudioSource>();

        if (grabDetection.isTheRadio)
        {
            StartCoroutine(RadioStationChangeRoutine());
        }
    }

    void Update()
    {
        // Only run radio logic if this is a radio
        if (!grabDetection.isTheRadio) return;

        isCurrentlyGrabbed = grabDetection.audioSource.isPlaying;

        // Detect when radio is newly grabbed
        bool justGrabbed = isCurrentlyGrabbed && !wasGrabbedLastFrame;
        wasGrabbedLastFrame = isCurrentlyGrabbed;

        if (justGrabbed && isReadyForNextClip)
        {
            StartCoroutine(PlayNextClipAfterStatic());
            isReadyForNextClip = false;
        }
    }

    void PlayStaticSound()
    {
        if (radioStaticSound != null && !hasPlayedStatic)
        {
            radioAudioSource.clip = radioStaticSound;
            radioAudioSource.Play();
            hasPlayedStatic = true;
        }
    }

    IEnumerator PlayNextClipAfterStatic()
    {
        // Play static first
        grabDetection.audioSource.clip = radioStaticSound;
        grabDetection.audioSource.Play();

        // Wait for static to finish
        yield return new WaitForSeconds(radioStaticSound.length);

        currentClipIndex = (currentClipIndex + 1) % radioClips.Length;

        // Play the next clip
        if (radioClips.Length > 0 && radioClips[currentClipIndex] != null)
        {
            grabDetection.audioSource.clip = radioClips[currentClipIndex];
            grabDetection.audioSource.Play();
        }

        hasPlayedStatic = false;
    }

    IEnumerator RadioStationChangeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(stationChangeInterval);

            PlayStaticSound();

            isReadyForNextClip = true;
        }
    }
}