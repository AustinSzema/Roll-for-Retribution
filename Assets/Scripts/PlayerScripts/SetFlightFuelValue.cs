using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetFlightFuelValue : MonoBehaviour
{
    [SerializeField] private Slider _flightFuelSlider;
    [SerializeField] private floatVariable _flightDuration;
    
    [SerializeField] private GameObject _flightSliderBackground;

    [SerializeField] private Magnet _magnet;
    
    [SerializeField] private Slider _minimumFuelSlider;

    [SerializeField] private String _fuelDefaultText = "Levitation Energy: ";
    [SerializeField] private TextMeshProUGUI _fuelTextMeshProUGUI;


    [SerializeField] private Image _fuelFill;


    [SerializeField] private boolVariable _outOfFuel;


    [SerializeField] private boolVariable _gameIsPaused;
    
    private Color _originalFillColor;

    private void Start()
    {
        _flightFuelSlider.maxValue = _magnet._maxFlightDuration;
        _minimumFuelSlider.maxValue = _magnet._maxFlightDuration;
        _originalFillColor = _fuelFill.color;
    }

    
    
    
    
    private void Update()
    {
        if (!_gameIsPaused.Value)
        {
            // Set the slider value
            _flightFuelSlider.value = _flightDuration.Value;
            _minimumFuelSlider.value = _magnet._fuelPenaltyThreshold;

            if (_outOfFuel.Value == true)
            {
                _fuelFill.color = _originalFillColor / 2;
            }

            if (_flightFuelSlider.value >= _magnet._fuelPenaltyThreshold)
            {
                Debug.Log("Flight Fuel Slider Value: " + _flightFuelSlider.value);
                _fuelFill.color = _originalFillColor;
            }

            if (_outOfFuel.Value == false)
            {
                _fuelTextMeshProUGUI.text = _fuelDefaultText + (int)(_flightFuelSlider.value / _flightFuelSlider.maxValue * 100) + "%";
            }
            else
            {
                _fuelTextMeshProUGUI.text = "Recharging...";
            }


            
        }

    }

    public void EnableSliderVisual()
    {
        _flightSliderBackground.SetActive(true);
    }
}
