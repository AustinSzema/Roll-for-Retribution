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
        pullForceCost.text = shop.CostToLevel(Shop.SkillsToLevel.PullForce).ToString();
        demonQuantityCost.text = shop.CostToLevel(Shop.SkillsToLevel.DemonQuantity).ToString();
        demonWeightCost.text = shop.CostToLevel(Shop.SkillsToLevel.DemonWeight).ToString();
        // Debug.Log(pullForceCost.text);
        // Debug.Log(diceQuantityCost.text);
        // Debug.Log(diceWeight.text);
        skillPoints.text = "Skill Points: "  + shop.CurrentSkillPoints().ToString();
    }
    
}
