using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] private WeaponList weaponList;

    [Space] [SerializeField] private List<Image> buttons = new List<Image>();

    private void Start()
    {
        if (buttons == null)
        {
            buttons = GetComponentsInChildren<Image>().ToList();
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Weapon weapon = weaponList.weaponList[i].GetComponent<Weapon>() ?? weaponList.weaponList[i].GetComponentInChildren<Weapon>();
            buttons[i].sprite = weapon.weaponUISprite;
        }
    }
}
