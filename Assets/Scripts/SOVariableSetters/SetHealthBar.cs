using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetHealthBar : MonoBehaviour
{
    [SerializeField] private intVariable _entityHealth;

    [SerializeField] private intVariable _entityMaxHp;

    [SerializeField] private Slider _slider;

    [SerializeField] private TextMeshProUGUI _healthText;

    private float _currentHealth;
    
    // Update is called once per frame
    void Update()
    {

        _currentHealth = (float)_entityHealth.Value / _entityMaxHp.Value;
        _slider.value = _currentHealth;
        if (_currentHealth >= 0f)
        {
            _healthText.text = "Health: " + _currentHealth * 100f + "%";
        }
        else
        {
            _healthText.text = "Health: 0%";
        }
    }
}
