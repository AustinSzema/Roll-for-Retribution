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

    [SerializeField] private Magnet magnet;

    private void Update()
    {
       UpdateFields();
    }

    // Update is called once per frame
    public void UpdateFields()
    {
        pullForce.text = magnet.PullSpeed.ToString();
        diceQuantity.text = magnet.GetMagnetCount().ToString();
        diceWeight.text = magnet.GetDiceWeight().ToString();
    }
    
}
