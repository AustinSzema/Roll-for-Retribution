using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [Tooltip("The AudioSource that plays continuous ambient sound")]
    public AudioSource ambientSource;

    [Tooltip("Optional separate AudioSource for sound effects (will create one if null)")]
    public AudioSource effectSource;

    [Header("Sound Effect Settings")]
    [Tooltip("Sound effects that will randomly play")]
    public AudioClip[] soundEffects;

    public float minTimeBetweenEffects = 60f;

    public float maxTimeBetweenEffects = 200f;

    [Tooltip("Volume range for sound effects (min, max)")]
    public Vector2 effectVolumeRange = new Vector2(0.5f, 1.0f);

    [Tooltip("Temporarily reduce ambient volume when effect plays")]
    public bool duckAmbientVolume = true;

    [Tooltip("How much to reduce ambient volume when effects play (0-1)")]
    [Range(0f, 1f)]
    public float duckAmount = 0.3f;

    public float duckFadeTime = 0.5f;

    private float originalAmbientVolume;
    private float nextEffectTime;
    private Coroutine volumeFadeCoroutine;

    void Start()
    {
        if (ambientSource == null)
        {
            ambientSource = GetComponent<AudioSource>();

            if (ambientSource == null)
            {
                Debug.LogError("No AudioSource found for ambient sound. Please assign one in the inspector.");
                enabled = false;
                return;
            }
        }

        if (effectSource == null)
        {
            // Create a new AudioSource for at the game object's point effects if none was provided
            effectSource = gameObject.AddComponent<AudioSource>();
            effectSource.playOnAwake = false;
            effectSource.loop = false;
        }

        originalAmbientVolume = ambientSource.volume;

        ScheduleNextEffect();
    }

    void Update()
    {
        if (DayManagerSingleton.Instance.day > 1)
        {
            if (Time.time >= (nextEffectTime/DayManagerSingleton.Instance.day) && soundEffects.Length > 0)
            {
                PlayRandomEffect();
                ScheduleNextEffect();
            }
        }
    }

    private void ScheduleNextEffect()
    {
        nextEffectTime = Time.time + Random.Range(minTimeBetweenEffects, maxTimeBetweenEffects);
    }

    private void PlayRandomEffect()
    {
        if (soundEffects.Length == 0) return;

        AudioClip effectToPlay = soundEffects[Random.Range(0, soundEffects.Length)];

        if (effectToPlay != null)
        {
            effectSource.volume = Random.Range(effectVolumeRange.x, effectVolumeRange.y);

            effectSource.PlayOneShot(effectToPlay);

            if (duckAmbientVolume)
            {
                DuckAmbientVolume(effectToPlay.length);
            }
        }
    }

    private void DuckAmbientVolume(float effectLength)
    {
        if (volumeFadeCoroutine != null)
        {
            StopCoroutine(volumeFadeCoroutine);
        }

        // Start a new sound fade coroutine (ambient sound down to make sound effect sound better)
        volumeFadeCoroutine = StartCoroutine(FadeAmbientVolume(
            originalAmbientVolume * (1f - duckAmount),
            originalAmbientVolume,                     
            duckFadeTime,                              
            effectLength - (duckFadeTime * 2),         
            duckFadeTime                               
        ));
    }

    private IEnumerator FadeAmbientVolume(float targetVolume, float returnVolume, float fadeDownTime, float holdTime, float fadeUpTime)
    {

        float startVolume = ambientSource.volume;

        float timer = 0;
        while (timer < fadeDownTime)
        {
            timer += Time.deltaTime;
            ambientSource.volume = Mathf.Lerp(startVolume, targetVolume, timer / fadeDownTime);
            yield return null;
        }
        ambientSource.volume = targetVolume;

        yield return new WaitForSeconds(holdTime);

        timer = 0;
        while (timer < fadeUpTime)
        {
            timer += Time.deltaTime;
            ambientSource.volume = Mathf.Lerp(targetVolume, returnVolume, timer / fadeUpTime);
            yield return null;
        }
        ambientSource.volume = returnVolume;

        volumeFadeCoroutine = null;
    }

    public void PlayEffect(AudioClip effect)
    {
        if (effect != null)
        {
            effectSource.PlayOneShot(effect);

            if (duckAmbientVolume)
            {
                DuckAmbientVolume(effect.length);
            }

            ScheduleNextEffect();
        }
    }
}
