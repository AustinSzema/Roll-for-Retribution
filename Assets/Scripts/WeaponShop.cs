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
            GameObject selectedWeapon = null;
            Weapon weapon = null;

            // Try to get a random weapon that is not in currentWeapons
            do
            {
                int random = Random.Range(0, internalWeaponsList.Count); // Include last element
                selectedWeapon = internalWeaponsList[random];
                weapon = WeaponManager.Instance.GetWeaponComponent(selectedWeapon);

            } while (IsWeaponInCurrentWeapons(selectedWeapon)); // Ensure the selected weapon isn't already in the current weapons list

            weaponOptions.Add(selectedWeapon);
            buttons[i].sprite = weapon.weaponUISprite;
            descriptions[i].text = weapon.weaponDescription;
            internalWeaponsList.Remove(selectedWeapon); // Remove selected weapon from list
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

    // Method to check if a weapon is already in the current weapons list
    private bool IsWeaponInCurrentWeapons(GameObject weapon)
    {
        foreach (Image weaponImage in currentWeapons)
        {
            Weapon currentWeapon = WeaponManager.Instance.GetWeaponComponent(WeaponManager.Instance.weaponParentList[currentWeapons.IndexOf(weaponImage)]);
            if (currentWeapon != null && currentWeapon.gameObject == weapon)
            {
                return true;
            }
        }
        return false;
    }
}
