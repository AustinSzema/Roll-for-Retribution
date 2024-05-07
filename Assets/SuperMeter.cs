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
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (_gameManager.canUseSuper)
        {
            _slider.value = _slider.maxValue;
        }
        else
        {
            _slider.value = _gameManager.killCount % 100f / 100f;
        }
        
        if (_gameManager.canUseSuper)
        {
            _pulseOutline.enabled = true;
        }
        else
        {
            _pulseOutline.enabled = false;
        }
    }
}
