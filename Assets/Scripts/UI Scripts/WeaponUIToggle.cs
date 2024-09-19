using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIToggle : MonoBehaviour
{

    private Color _shotgunEnabledColor = Color.white;
    private Color _shotgunDisabledColor = Color.grey;
    
    private Color _rocketEnabledColor = Color.white;
    private Color _rocketDisabledColor = Color.grey;

    private bool useShotgunColor = true; // Indicates whether shotgun color is currently being used

    [SerializeField] private List<GameObject> shotTypeUIParents = new List<GameObject>();

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;

        _shotgunEnabledColor = shotTypeUIParents[0].GetComponent<Image>().color;
        _shotgunDisabledColor = _shotgunEnabledColor * 0.5f; // Darken the color by multiplying by a factor (0.5 here)
        _shotgunDisabledColor.a = 1f; // Ensure alpha remains 1

        _rocketEnabledColor = shotTypeUIParents[1].GetComponent<Image>().color;
        _rocketDisabledColor = _rocketEnabledColor * 0.5f; // Darken the color by multiplying by a factor (0.5 here)
        _rocketDisabledColor.a = 1f; // Ensure alpha remains 1
    }

    // void Update()
    // {
    //     switch (_gameManager.activeShot)
    //     {
    //         case GameManager.ActiveShotType.Shotgun:
    //             useShotgunColor = true;
    //             EnableActive(0);
    //             break;
    //         case GameManager.ActiveShotType.Rocket:
    //             useShotgunColor = false;
    //             EnableActive(1);
    //             break;
    //         case GameManager.ActiveShotType.Spray:
    //             EnableActive(2);
    //             break;
    //         case GameManager.ActiveShotType.Beam:
    //             EnableActive(3);
    //             break;
    //     }
    // }

    void EnableActive(int index)
    {
        foreach (GameObject obj in shotTypeUIParents)
        {
            Image img = obj.GetComponent<Image>();
            if (useShotgunColor)
            {
                img.color = _rocketDisabledColor;

            }
            else
            {
                img.color = _shotgunDisabledColor;
            }
        }

        Image activeWeaponImage = shotTypeUIParents[index].GetComponent<Image>();
        if (useShotgunColor)
        {
            activeWeaponImage.color = _shotgunEnabledColor;
        }
        else
        {
            activeWeaponImage.color = _rocketEnabledColor;
        }
    }
}
