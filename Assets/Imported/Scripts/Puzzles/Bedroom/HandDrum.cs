using System.Collections;
using System.Collections.Generic;
using Blyman94.CommonSolutions;
using UnityEngine;

public class HandDrum : MonoBehaviour
{
    public Animator DrumAnimator;
    [SerializeField] private IntVariable _numberOfTaps;
    [SerializeField] private float _lowerDuration = 1.0f;
    [SerializeField] private AudioSource _drumAudio;
    [SerializeField] private AudioClip _drumClip;
    private bool _canStrike = true;
    public void PlaySound()
    {
        _drumAudio.pitch = Random.Range(0.9f, 1.1f);
        _drumAudio.PlayOneShot(_drumClip);
    }
    public void Strike()
    {
        if (_canStrike)
        {
            DrumAnimator.SetTrigger("Strike");
            _canStrike = false;
        }
    }
    public void ResetCanStrike()
    {
        _canStrike = true;
        _numberOfTaps.Value += 1;
    }
    public void Lower()
    {
        _canStrike = false;
        DrumAnimator.SetTrigger("Lower");
        StartCoroutine(DeactivateRoutine());
    }
    private IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(_lowerDuration);
        gameObject.SetActive(false);
    }
}
