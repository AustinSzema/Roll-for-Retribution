using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperMeter : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField] private Slider _slider;

    [SerializeField] private PulseOutline _pulseOutline;

    [SerializeField] private Outline _outline;

    private void Start()
    {
        _outline.enabled = false;
        _gameManager = GameManager.Instance;
        _slider.minValue = 0;
        _slider.maxValue = _gameManager.superMeterActivationAmount;
    }

    private void Update()
    {
        if (_gameManager.canUseSuper)
        {
            _slider.value = _slider.maxValue;
        }
        else
        {
            _slider.value = _gameManager.enemiesHit;
        }

        _outline.enabled = _gameManager.canUseSuper;
        _pulseOutline.enabled = _gameManager.canUseSuper;
        
        EZDebug.Log("PlayerHits: " + _gameManager.enemiesHit);
    }
}
