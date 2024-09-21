using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapWeapon : MonoBehaviour
{
    private List<Image> weaponSlots = new List<Image>();
    [SerializeField] private List<GameObject> weaponParents = new List<GameObject>();
    
    private int activeWeaponIndex = 0;

    
    
    
    void Start()
    {
        // Populate the weapon slots with Image components
        for (int i = 0; i < transform.childCount; ++i)
        {
            Image weaponSlot = transform.GetChild(i).GetComponent<Image>();
            if (weaponSlot != null)
            {
                weaponSlots.Add(weaponSlot);
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