using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFlightFuelValue : MonoBehaviour
{
    [SerializeField] private Slider _flightFuelSlider;
    [SerializeField] private floatVariable _flightDuration;
    
    [SerializeField] private GameObject _flightSliderBackground;

    private float _maxFlightDuration;

    private void Start()
    {
        _maxFlightDuration = _flightDuration.Value;
    }

    private void Update()
    {
        // Normalize flight duration between 0 and 1
        float normalizedValue = _flightDuration.Value / _maxFlightDuration;

        // Ensure the value is clamped between 0 and 1
        normalizedValue = Mathf.Clamp01(normalizedValue);

        // Set the slider value
        _flightFuelSlider.value = normalizedValue;

        if (Input.GetKey(KeyCode.Space) && _flightDuration.Value < _maxFlightDuration)
        {
            _flightSliderBackground.SetActive(true);
        }
        else
        {
            _flightSliderBackground.SetActive(false);
        }
    }

    public void EnableSliderVisual()
    {
        _flightSliderBackground.SetActive(true);
    }
}
