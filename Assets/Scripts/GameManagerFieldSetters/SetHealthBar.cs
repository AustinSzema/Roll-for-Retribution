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

    
    private float _currentPlayerHealth;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }


    // Update is called once per frame
    void Update()
    {

        _currentPlayerHealth = (float)_gameManager.playerCurrentHealth;
        _slider.value = _currentPlayerHealth / 100;
        if (_currentPlayerHealth >= 0f)
        {
            int healthValue = Mathf.RoundToInt(_currentPlayerHealth);
            _healthText.text = "Health " + healthValue;
        }
        else
        {
            _healthText.text = "Health: 0";
        }
    }
}
