using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [SerializeField] private TextMeshProUGUI _healthText;

    
    private float _currentHealth;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }


    // Update is called once per frame
    void Update()
    {

        _currentHealth = (float)_gameManager.playerCurrentHealth / _gameManager.playerMaxHealth;
        _slider.value = _currentHealth;
        if (_currentHealth >= 0f)
        {
            _healthText.text = "Health " + _currentHealth * 100f + "%";
        }
        else
        {
            _healthText.text = "Health: 0%";
        }
    }
}
