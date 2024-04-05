using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIToggle : MonoBehaviour
{
    [SerializeField] private ShotTypeSO _activeShotTypeSO;


    [SerializeField] private Color _enabledColor = Color.white;
    [SerializeField] private Color _disabledColor = Color.grey;

    [SerializeField] private List<GameObject> shotTypeUIParents = new List<GameObject>();

    void Update()
    {

        switch (_activeShotTypeSO.activeShotType)
        {
            case ShotTypeSO.ShotType.Shotgun:
                EnableActive(0);
                break;
            case ShotTypeSO.ShotType.Sniper:
                EnableActive(1);
                break;
            case ShotTypeSO.ShotType.Spray:
                EnableActive(2);
                break;
            case ShotTypeSO.ShotType.Beam:
                EnableActive(3);
                break;
        }
    }

    void EnableActive(int index)
    {
        foreach (GameObject obj in shotTypeUIParents)
        {
            obj.GetComponent<Image>().color = _disabledColor;
            obj.GetComponentInChildren<TextMeshProUGUI>().color = _disabledColor;
        }   
        shotTypeUIParents[index].GetComponent<Image>().color = _enabledColor;
        shotTypeUIParents[index].GetComponentInChildren<TextMeshProUGUI>().color = _enabledColor;

    }
}
