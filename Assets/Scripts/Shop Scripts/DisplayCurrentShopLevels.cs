using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCurrentShopLevels : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI diceQuantity;
    
    [SerializeField]
    private TextMeshProUGUI diceWeight;
    
    [SerializeField]
    private TextMeshProUGUI pullForce;
    
    [SerializeField]
    private TextMeshProUGUI diceQuantityCost;
    
    [SerializeField]
    private TextMeshProUGUI diceWeightCost;
    
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
        diceQuantity.text = magnet.GetMagnetCount().ToString();
        diceWeight.text = Math.Round(magnet.GetDiceWeight(), 2).ToString();
        pullForceCost.text = shop.CostToLevel(Shop.SkillsToLevel.PullForce).ToString();
        diceQuantityCost.text = shop.CostToLevel(Shop.SkillsToLevel.DiceQuantity).ToString();
        diceWeightCost.text = shop.CostToLevel(Shop.SkillsToLevel.DiceWeight).ToString();
        // Debug.Log(pullForceCost.text);
        // Debug.Log(diceQuantityCost.text);
        // Debug.Log(diceWeight.text);
        skillPoints.text = "Skill Points: "  + shop.CurrentSkillPoints().ToString();
    }
    
}
