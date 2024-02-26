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
    
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = (float) _entityHealth.Value / _entityMaxHp.Value;
        _healthText.text = "Health: " + (float)_entityHealth.Value / _entityMaxHp.Value * 100f + "%";
    }
}
