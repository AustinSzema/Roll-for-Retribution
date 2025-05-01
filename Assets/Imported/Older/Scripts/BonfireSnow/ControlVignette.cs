using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ControlVignette : MonoBehaviour
{
    [SerializeField] private Volume _volume;
    [SerializeField] private float _vignetteIntensity = 0.55f;
    private Vignette _vignette;

    private void Awake()
    {
        _volume.profile.TryGet<Vignette>(out _vignette);
    }

    public void ShowVignette()
    {
        _vignette.intensity.value = _vignetteIntensity;
    }

    public void HideVignette()
    {
        _vignette.intensity.value = 0.0f;
    }
}
