using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] private WeaponList weaponList;

    [SerializeField] private GameObject loadingText;
    
    [SerializeField] private List<Image> currentWeapons = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> currentDescriptions = new List<TextMeshProUGUI>();

    
    [Space] [SerializeField] private List<Image> buttons = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> descriptions = new List<TextMeshProUGUI>();

    private List<GameObject> weaponOptions = new List<GameObject>();

    
    private List<GameObject> internalWeaponsList = new List<GameObject>();
    private void OnEnable()
    {
        for (int i = 0; i < currentWeapons.Count; i++)
        {
            currentWeapons[i].sprite = WeaponManager.Instance.GetWeaponComponent(WeaponManager.Instance.weaponParentList[i]).weaponUISprite; //TODO: this sucks
        }

        
        foreach (GameObject obj in weaponList.weaponList)
        {
            internalWeaponsList.Add(obj);
        }
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        for (int i = 0; i < buttons.Count; i++)
        {
            int random = Random.Range(0, internalWeaponsList.Count-1);
            Weapon weapon = WeaponManager.Instance.GetWeaponComponent(internalWeaponsList[random]);
            weaponOptions.Add(internalWeaponsList[random]);
            buttons[i].sprite = weapon.weaponUISprite;
            descriptions[i].text = weapon.weaponDescription;
            internalWeaponsList.Remove(internalWeaponsList[random]);
        }
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
   
    }

    public void NextScene(int index)
    {
        loadingText.SetActive(true);
        WeaponManager.Instance.weaponParentList[index] = weaponOptions[index];
        GameManager.Instance.currentRound++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
