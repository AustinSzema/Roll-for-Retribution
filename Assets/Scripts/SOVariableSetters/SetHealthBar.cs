using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHealthBar : MonoBehaviour
{
    [SerializeField] private intVariable _entityHealth;

    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider.maxValue = _entityHealth.Value;
    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(_entityHealth.name + " : " + _entityHealth.Value);

        _slider.value = _entityHealth.Value;
    }
}
