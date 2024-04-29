using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIToggle : MonoBehaviour
{

    [SerializeField] private Color _enabledColor = Color.white;
    [SerializeField] private Color _disabledColor = Color.grey;

    [SerializeField] private List<GameObject> shotTypeUIParents = new List<GameObject>();

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    void Update()
    {

        switch (_gameManager.activeShot)
        {
            case GameManager.ActiveShotType.Shotgun:
                EnableActive(0);
                break;
            case GameManager.ActiveShotType.Rocket:
                EnableActive(1);
                break;
            case GameManager.ActiveShotType.Spray:
                EnableActive(2);
                break;
            case GameManager.ActiveShotType.Beam:
                EnableActive(3);
                break;
        }
    }

    void EnableActive(int index)
    {
        foreach (GameObject obj in shotTypeUIParents)
        {
            obj.GetComponent<Image>().color = _disabledColor;
            //obj.GetComponentInChildren<TextMeshProUGUI>().color = _disabledColor;
        }   
        shotTypeUIParents[index].GetComponent<Image>().color = _enabledColor;
        //shotTypeUIParents[index].GetComponentInChildren<TextMeshProUGUI>().color = _enabledColor;
        
    }
}
