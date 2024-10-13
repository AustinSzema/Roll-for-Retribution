using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwapWeapon : MonoBehaviour
{
    [SerializeField] private Transform weaponSwapUI;
    [SerializeField] private GameObject weaponSlot;
    private List<Image> weaponSlots = new List<Image>();
    [SerializeField] private List<GameObject> weaponParents = new List<GameObject>();

    private int activeWeaponIndex = 0;

    void Start()
    {
        for (int i = 0; i < weaponParents.Count; i++)
        {
            GameObject weaponUI = Instantiate(weaponSlot, weaponSwapUI);
            Image weaponImage = weaponUI.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI slotNumber = weaponUI.GetComponentInChildren<TextMeshProUGUI>();

            // Display slot numbers: 1-9 for the first 9 weapons, E for the 10th weapon
            if (i < 9) // For indices 0-8 (1-9 in UI)
            {
                slotNumber.text = "[" + (i + 1) + "]";
            }
            else // For index 9 (10th weapon)
            {
                slotNumber.text = "[E]"; // Display E for the 10th weapon
            }

            // Assign weapon image sprite
            if (weaponParents[i].GetComponent<Weapon>() == null)
            {
                weaponImage.sprite = weaponParents[i].GetComponentInChildren<Weapon>().weaponUISprite;
            }
            else
            {
                weaponImage.sprite = weaponParents[i].GetComponent<Weapon>().weaponUISprite;
            }

            if (weaponImage != null)
            {
                weaponSlots.Add(weaponUI.GetComponent<Image>());
            }
        }

        UpdateWeaponDisplay();
    }

    void Update()
    {
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            // Check for number keys 1-9 which correspond to weapon indices 0-8
            if (i < 9 && Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                activeWeaponIndex = i; // Set to weapon index 0-8
                UpdateWeaponAndPosition();
                break; // Only update once per frame
            }

            // Check for E key for the 10th weapon (index 9)
            if (i == 9 && Input.GetKeyDown(KeyCode.E))
            {
                activeWeaponIndex = i; // Set to weapon index 9
                UpdateWeaponAndPosition();
                Debug.Log($"Switched to weapon index: {activeWeaponIndex} using E key.");
                break; // Only update once per frame
            }
        }
    }

    private void UpdateWeaponAndPosition()
    {
        UpdateWeaponDisplay();

        // Move the weapons to the designated hand position
        foreach (Weapon w in GameManager.Instance.weapons)
        {
            w.transform.position = GameManager.Instance.handPosition;
        }
    }

    private void UpdateWeaponDisplay()
    {
        // Update the weapon slots display
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            weaponSlots[i].color = (i == activeWeaponIndex) ? Color.white : Color.grey;
        }

        // Activate the appropriate weapon parent
        for (int i = 0; i < weaponParents.Count; i++)
        {
            weaponParents[i].SetActive(i == activeWeaponIndex);
        }
    }
}
