using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwapWeapon : MonoBehaviour
{
    
    [SerializeField] private WeaponList weaponList;
    [SerializeField] private List<Image> weaponSlots = new List<Image>();
    [SerializeField] private List<Image> weaponIcons = new List<Image>();

    private List<GameObject> weaponParents = new List<GameObject>();

    


    private int activeWeaponIndex = 0;
    // private readonly KeyCode[] weaponKeys = {
    //     KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
    //     KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0,
    //     KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U,
    //     KeyCode.I, KeyCode.O, KeyCode.P, KeyCode.LeftBracket, KeyCode.RightBracket
    // };
    private readonly KeyCode[] weaponKeys = {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3
    };

    private Weapon GetWeaponComponent(GameObject obj)
    {
        return obj.GetComponent<Weapon>() ?? obj.GetComponentInChildren<Weapon>();
    }
    

    void Start()
    {

        for (int i = 0; i < weaponIcons.Count; i++)
        {
            GameObject weapon = Instantiate(weaponList.weaponList[i], transform);
            
            weaponParents.Add(weapon);
            
            weapon.SetActive(false);

            weaponIcons[i].sprite = GetWeaponComponent(weaponParents[i]).weaponUISprite; // I hate this
        }
        
        GameManager.Instance.weapons = FindObjectsOfType<Weapon>(true).ToList();
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
