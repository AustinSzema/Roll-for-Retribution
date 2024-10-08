using System.Collections.Generic;
using System.Linq;
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
            Image weaponImage = weaponUI.transform.GetChild(0).GetComponent<Image>(); // stinky
            TextMeshProUGUI slotNumber = weaponUI.GetComponentInChildren<TextMeshProUGUI>();

            slotNumber.text = "[" + (i + 1) + "]";
            if (weaponParents[i].GetComponent<Weapon>() == null)
            {
                
                weaponImage.sprite = weaponParents[i].GetComponentInChildren<Weapon>().weaponUISprite; // TODO: this sucks
            }
            else
            {
                weaponImage.sprite = weaponParents[i].GetComponent<Weapon>().weaponUISprite; // TODO: this sucks
            }
            


            if (weaponImage != null)
            {
                weaponSlots.Add(weaponImage);
            }

        }
        
        UpdateWeaponDisplay();
    }

    void Update()
    {
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Use KeyCode directly
            {
                activeWeaponIndex = i;
                UpdateWeaponDisplay();
                break; // Only update once per frame
            }
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