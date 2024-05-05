using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DisplayCurrentShopLevels : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI demonQuantity;
    
    [SerializeField] private TextMeshProUGUI demonWeight;
    
    [SerializeField]
    private TextMeshProUGUI pullForce;
    
    [SerializeField] private TextMeshProUGUI demonQuantityCost;
    
    [SerializeField] private TextMeshProUGUI demonWeightCost;
    
    [SerializeField]
    private TextMeshProUGUI pullForceCost;


    [SerializeField] private TextMeshProUGUI skillPoints;

    [SerializeField] private Magnet magnet;

    [SerializeField] private Shop shop;

    private string _soldOut = "Sold Out";
    
    private void Update()
    {
       UpdateFields();
    }

    // Update is called once per frame
    public void UpdateFields()
    {
        pullForce.text = magnet._pullSpeed.ToString();
        demonQuantity.text = magnet.GetMagnetCount().ToString();
        demonWeight.text = Math.Round(magnet.GetDemonWeight(), 2).ToString();
        
        pullForce.text = magnet._pullSpeed.ToString();
        demonQuantity.text = magnet.GetMagnetCount().ToString();
        demonWeight.text = Math.Round(magnet.GetDemonWeight(), 2).ToString();

        UpdateCostField(Shop.SkillsToLevel.PullForce, pullForceCost);
        UpdateCostField(Shop.SkillsToLevel.DemonQuantity, demonQuantityCost);
        UpdateCostField(Shop.SkillsToLevel.DemonWeight, demonWeightCost);

        skillPoints.text = "Skill Points: " + shop.CurrentSkillPoints().ToString();
    }
    
    private void UpdateCostField(Shop.SkillsToLevel skill, TextMeshProUGUI text)
    {
        if (shop.CostToLevel(skill) >= Int32.MaxValue) // TODO: should this use something other than Int32.MaxValue as a way to determine if there are no more upgrades
        {
            text.text = _soldOut;
        }
        else
        {
            text.text = shop.CostToLevel(skill).ToString();
        }
    }
}
