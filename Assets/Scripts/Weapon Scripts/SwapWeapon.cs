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
    private List<GameObject> weaponParents = new List<GameObject>();

    [SerializeField] private WeaponList weaponList;

    private int activeWeaponIndex = 0;
    private readonly KeyCode[] weaponKeys = {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
        KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0,
        KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U,
        KeyCode.I, KeyCode.O, KeyCode.P, KeyCode.LeftBracket, KeyCode.RightBracket
    };

    private Weapon GetWeaponComponent(GameObject obj)
    {
        return obj.GetComponent<Weapon>() ?? obj.GetComponentInChildren<Weapon>();
    }
    

    void Start()
    {
        foreach (GameObject obj in weaponList.weaponList)
        {
            GameObject weapon = Instantiate(obj, transform);
            
            weaponParents.Add(weapon);
            weapon.SetActive(false);
            


        }
        
        GameManager.Instance.weapons = FindObjectsOfType<Weapon>(true).ToList();

        for (int i = 0; i < weaponParents.Count; i++)
        {
            GameObject weaponUI = Instantiate(weaponSlot, weaponSwapUI);
            Image weaponImage = weaponUI.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI slotNumber = weaponUI.GetComponentInChildren<TextMeshProUGUI>();

            // Set slot label based on weapon index and assigned key
            slotNumber.text = "[" + GetWeaponSlotLabel(i) + "]";

            
            // Assign weapon image sprite
            var weaponComponent = GetWeaponComponent(weaponParents[i]);
            if (weaponComponent != null)
            {
                weaponImage.sprite = weaponComponent.weaponUISprite;
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
        for (int i = 0; i < weaponKeys.Length; i++)
        {
            if (Input.GetKeyDown(weaponKeys[i]))
            {
                activeWeaponIndex = i; // Set to corresponding weapon index
                UpdateWeaponAndPosition();
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

    private string GetWeaponSlotLabel(int index)
    {
        if (index < 9) return (index + 1).ToString(); // 1-9
        else if (index == 9) return "0"; // 10th slot is '0'
        else return weaponKeys[index].ToString(); // E, R, T, Y, U, I, O, P, [, ]
    }
}
