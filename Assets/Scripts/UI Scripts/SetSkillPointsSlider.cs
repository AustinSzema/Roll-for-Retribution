using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SetSkillPointsSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Shop _shop;

    private int _pullForceCost;
    private int _diceQuantityCost;
    private int _diceWeightCost;

    private List<int> _costs = new List<int>();

    [SerializeField] private Outline _outline;
    [SerializeField] private PulseOutline _pulseOutline;

    [SerializeField] private GameObject _upgradeNotification;

    
    void Update()
    {
        _pullForceCost = int.Parse(_shop.CostToLevel(Shop.SkillsToLevel.PullForce).ToString());
        _diceQuantityCost = int.Parse(_shop.CostToLevel(Shop.SkillsToLevel.DemonQuantity).ToString());
        _diceWeightCost = int.Parse(_shop.CostToLevel(Shop.SkillsToLevel.DemonWeight).ToString());

        _costs.Clear(); // Clear the list before adding new costs
        _costs.Add(_pullForceCost);
        _costs.Add(_diceQuantityCost);
        _costs.Add(_diceWeightCost);
        
        // This clamps the slider's value between 0 and the lowest upgrade cost.
        // So when the player can purchase an upgrade in the shop, the slider is full.
        _slider.maxValue = _costs.Min();
        _slider.value = _shop.CurrentSkillPoints();
        if (_slider.value >= _slider.maxValue && _slider.maxValue >= 0)
        {
            _outline.enabled = true;
            _pulseOutline.enabled = true;
            _upgradeNotification.SetActive(true);
        }
        else
        {
            _outline.enabled = false;
            _pulseOutline.enabled = false;
            //_upgradeNotification.SetActive(false);

        }
    }
}