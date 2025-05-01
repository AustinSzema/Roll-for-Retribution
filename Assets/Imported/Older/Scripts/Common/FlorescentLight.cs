using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorescentLight : MonoBehaviour
{
    [SerializeField] Light[] _pointLights;
    [SerializeField] GameObject _glowingLight;
    [SerializeField] GameObject _notGlowingLight;

    public void TurnOffLights()
    {
        foreach (Light light in _pointLights)
        {
            light.enabled = false;
            _glowingLight.SetActive(false);
            _notGlowingLight.SetActive(true);
        }
    }

    public void TurnOnLights()
    {
        foreach (Light light in _pointLights)
        {
            light.enabled = true;
            _glowingLight.SetActive(true);
            _notGlowingLight.SetActive(false);
        }
    }

}
