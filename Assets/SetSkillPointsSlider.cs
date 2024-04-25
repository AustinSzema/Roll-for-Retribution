using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSkillPointsSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [SerializeField] private Shop _shop;
    
    // Update is called once per frame
    void Update()
    {
        _slider.value = Mathf.Clamp01(_shop.CurrentSkillPoints()); 
    }
}