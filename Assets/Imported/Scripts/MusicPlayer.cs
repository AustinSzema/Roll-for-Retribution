using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicPlayer : MonoBehaviour
{

    public static MusicPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }

    public AudioSource audioSource;
    IEnumerator Start()
    {
        while (true)
        {
            audioSource.pitch = Random.Range(1f, 1.3f);
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }

}
