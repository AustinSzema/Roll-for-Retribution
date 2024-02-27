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

    [SerializeField] private Magnet _magnet;
    
    [SerializeField] private Slider _minimumFuelSlider;
    
    private void Start()
    {
        _flightFuelSlider.maxValue = _magnet._maxFlightDuration;
        _minimumFuelSlider.maxValue = _magnet._maxFlightDuration;
    }

    private float _previousFuel = 0;
    
    
    
    private void Update()
    {
        // Set the slider value
        _flightFuelSlider.value = _flightDuration.Value;
        _minimumFuelSlider.value = _magnet._minimumFuelAmount;


        
        // if the slider's value is not changing, do not show the slider. Else, show it
        if (_flightDuration.Value - _previousFuel <= 0.001f)
        {
            //_flightSliderBackground.SetActive(false);
        }
        else
        {
            _flightSliderBackground.SetActive(true);
        }
        /*
        if (_playerIsFlying.Value && _flightDuration.Value < _magnet._maxFlightDuration)
        {
            _flightSliderBackground.SetActive(true);
        }
        */
        /*if(_flightDuration.Value - _magnet._maxFlightDuration <= 0.001f)
        {
            _flightSliderBackground.SetActive(false);
        }*/

        // Must go at end of update so it can be checked next frame
        _previousFuel = _flightDuration.Value;
    }

    public void EnableSliderVisual()
    {
        _flightSliderBackground.SetActive(true);
    }
}
