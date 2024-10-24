using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SwapWeapon : MonoBehaviour
{
    [SerializeField] private WeaponList mainWeaponList;
    [SerializeField] private WeaponList startingWeaponList;

    [SerializeField] private List<Image> weaponSlots = new List<Image>();
    [SerializeField] private List<Image> weaponIcons = new List<Image>();
    private List<GameObject> weaponParents = new List<GameObject>();

    private int activeWeaponIndex = 0;

    
    
    // Limiting the number of keys to match potential weapon slots/icons
    private readonly KeyCode[] weaponKeys = {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3
    };

    void Start()
    {
        
        // Ensure we don't go out of bounds by taking the smallest count between weaponIcons and weaponList
        int weaponCount = Mathf.Min(weaponIcons.Count, mainWeaponList.weaponList.Count);

        for (int i = 0; i < weaponCount; i++)
        {
            GameObject weapon;
            if (GameManager.Instance.currentRound == 0)
            {
                // Instantiate and add weapons to weaponParents
                weapon = Instantiate(startingWeaponList.weaponList[i], transform);


                // Add weapon to WeaponManager if it's not already in the list
                if (WeaponManager.Instance.weaponParentList.Count == i)
                {
                    WeaponManager.Instance.weaponParentList.Add(startingWeaponList.weaponList[i]);
                }

            }
            else
            {
                weapon = Instantiate(WeaponManager.Instance.weaponParentList[i], transform);

                // Add weapon to WeaponManager if it's not already in the list
                if (WeaponManager.Instance.weaponParentList.Count == i)
                {
                    WeaponManager.Instance.weaponParentList.Add(mainWeaponList.weaponList[i]);
                }

            }
            weaponParents.Add(weapon);
            // Disable the weapon object initially
            weapon.SetActive(false);

            // Assign the weapon icon based on the corresponding weapon
            weaponIcons[i].sprite = WeaponManager.Instance.GetWeaponComponent(WeaponManager.Instance.weaponParentList[i]).weaponUISprite;
            
        }

        // Collect all weapons and update the display
        GameManager.Instance.weapons = FindObjectsOfType<Weapon>(true).ToList();
        UpdateWeaponDisplay();
    }

    void Update()
    {
        // Check if any weapon key was pressed
        for (int i = 0; i < weaponKeys.Length; i++)
        {
            if (Input.GetKeyDown(weaponKeys[i]))
            {
                activeWeaponIndex = i; // Set active weapon index to corresponding weapon
                UpdateWeaponAndPosition();
                break; // Only update once per frame
            }
        }
    }

    private void UpdateWeaponAndPosition()
    {
        UpdateWeaponDisplay();

        // Move all weapons to the designated hand position
        foreach (Weapon w in GameManager.Instance.weapons)
        {
            w.transform.position = GameManager.Instance.handPosition;
        }
    }

    private void UpdateWeaponDisplay()
    {
        // Update the weapon slots UI color based on the active weapon
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            weaponSlots[i].color = (i == activeWeaponIndex) ? Color.white : Color.grey;
        }

        // Activate the appropriate weapon parent based on the active weapon index
        for (int i = 0; i < weaponParents.Count; i++)
        {
            weaponParents[i].SetActive(i == activeWeaponIndex);
        }
    }

    private string GetWeaponSlotLabel(int index)
    {
        if (index < 9) return (index + 1).ToString(); // 1-9 for first 9 slots
        else if (index == 9) return "0"; // 10th slot is labeled '0'
        else return weaponKeys[index].ToString(); // Any additional keys like E, R, T, etc.
    }
}
