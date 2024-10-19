using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        // Reset internal weapons list each time OnEnable is called
        internalWeaponsList.Clear();
        foreach (GameObject obj in weaponList.weaponList)
        {
            internalWeaponsList.Add(obj);
        }

        // Update current weapons displayed
        for (int i = 0; i < currentWeapons.Count; i++)
        {
            currentWeapons[i].sprite = WeaponManager.Instance.GetWeaponComponent(WeaponManager.Instance.weaponParentList[i]).weaponUISprite;
        }

        // Select random weapons for the buttons
        weaponOptions.Clear();
        for (int i = 0; i < buttons.Count; i++)
        {
            int random = Random.Range(0, internalWeaponsList.Count); // Include last element
            Weapon weapon = WeaponManager.Instance.GetWeaponComponent(internalWeaponsList[random]);
            weaponOptions.Add(internalWeaponsList[random]);
            buttons[i].sprite = weapon.weaponUISprite;
            descriptions[i].text = weapon.weaponDescription;
            internalWeaponsList.RemoveAt(random); // Remove selected weapon from list
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void NextScene(int slotIndex)
    {
        loadingText.SetActive(true);
        
        // Replace the weapon in the chosen slot with the selected weapon from the shop
        int randomWeaponIndex = buttons.IndexOf(buttons[slotIndex]);
        WeaponManager.Instance.weaponParentList[slotIndex] = weaponOptions[randomWeaponIndex];
        
        GameManager.Instance.currentRound++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
