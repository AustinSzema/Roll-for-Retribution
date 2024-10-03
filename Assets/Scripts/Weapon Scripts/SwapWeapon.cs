using System.Collections.Generic;
using System.Linq;
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
            Image slot = Instantiate(weaponSlot, weaponSwapUI).GetComponent<Image>();
            slot.sprite = weaponParents[i].GetComponentInChildren<Weapon>().weaponUISprite; // TODO: this sucks

            if (slot != null)
            {
                weaponSlots.Add(slot);
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