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

    [Space] [SerializeField] private List<Image> buttons = new List<Image>();
    [Space] [SerializeField] private List<TextMeshProUGUI> descriptions = new List<TextMeshProUGUI>();

    [SerializeField] private GameObject loadingText;
    
    private List<GameObject> internalWeaponsList = new List<GameObject>();

    private void OnEnable()
    {
        foreach (GameObject obj in weaponList.weaponList)
        {
            internalWeaponsList.Add(obj);
        }
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        for (int i = 0; i < buttons.Count; i++)
        {
            int random = Random.Range(0, internalWeaponsList.Count-1);
            Weapon weapon = internalWeaponsList[random].GetComponent<Weapon>() ?? internalWeaponsList[random].GetComponentInChildren<Weapon>();
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

    public void NextScene()
    {
        loadingText.SetActive(true);
        GameManager.Instance.currentRound++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
