using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHealthBar : MonoBehaviour
{
    [SerializeField] private intVariable _entityHealth;

    [SerializeField] private intVariable _entityMaxHp;

    [SerializeField] private Slider _slider;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = (float) _entityHealth.Value / (float) _entityMaxHp.Value;
    }
}
