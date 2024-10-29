using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] private WeaponList weaponList;
    [SerializeField] private GameObject loadingText;

    [FormerlySerializedAs("currentWeapons")] [SerializeField]
    private List<Image> currentWeaponsImages = new List<Image>();

    [SerializeField] private List<TextMeshProUGUI> currentDescriptions = new List<TextMeshProUGUI>();

    [FormerlySerializedAs("buttons")] [Space] [SerializeField]
    private List<Image> availableWeaponsImages = new List<Image>();

    [FormerlySerializedAs("descriptions")] [SerializeField]
    private List<TextMeshProUGUI> availableWeaponsDescriptions = new List<TextMeshProUGUI>();

    private List<GameObject> weaponOptions = new List<GameObject>();
    private List<GameObject> internalWeaponsList = new List<GameObject>();

    [SerializeField] private List<TextMeshProUGUI> arrowDescriptions = new List<TextMeshProUGUI>();

    private bool hasSwappedWeapon = false;

    private List<Sprite> currentSprites = new List<Sprite>();
    private List<Sprite> availableSprites = new List<Sprite>();


    private void OnEnable()
    {
        GameManager.Instance.gameIsPaused = true;
        // Reset internal weapons list each time OnEnable is called
        internalWeaponsList.Clear();
        foreach (GameObject obj in weaponList.weaponList)
        {
            internalWeaponsList.Add(obj);
        }


        // Update current weapons displayed
        for (int i = 0; i < currentWeaponsImages.Count; i++)
        {
            currentWeaponsImages[i].sprite = WeaponManager.Instance
                .GetWeaponComponent(WeaponManager.Instance.weaponParentList[i]).weaponUISprite;
            currentDescriptions[i].text = WeaponManager.Instance
                .GetWeaponComponent(WeaponManager.Instance.weaponParentList[i]).weaponDescription;
        }

        // Select random weapons for the buttons
        weaponOptions.Clear();
        for (int i = 0; i < availableWeaponsImages.Count; i++)
        {
            GameObject selectedWeapon = null;
            Weapon weapon = null;

            // Try to get a random weapon that is not in currentWeapons
            do
            {
                int random = Random.Range(0, internalWeaponsList.Count); // Include last element
                selectedWeapon = internalWeaponsList[random];
                weapon = WeaponManager.Instance.GetWeaponComponent(selectedWeapon);
            } while
                (IsWeaponInCurrentWeapons(
                    selectedWeapon)); // Ensure the selected weapon isn't already in the current weapons list

            weaponOptions.Add(selectedWeapon);
            availableWeaponsImages[i].sprite = weapon.weaponUISprite;
            availableWeaponsDescriptions[i].text = weapon.weaponDescription;
            arrowDescriptions[i].text = "Replace " + WeaponManager.Instance
                                            .GetWeaponComponent(WeaponManager.Instance.weaponParentList[i]).weaponName +
                                        " with " +
                                        weapon.weaponName;


            internalWeaponsList.Remove(selectedWeapon); // Remove selected weapon from list
        }

        for (int i = 0; i < currentWeaponsImages.Count; i++)
        {
            currentSprites.Add(currentWeaponsImages[i].sprite);
        }

        for (int i = 0; i < availableWeaponsImages.Count; i++)
        {
            availableSprites.Add(availableWeaponsImages[i].sprite);
        }


        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SwapToNewWeapon(int slotIndex)
    {
        // Replace the weapon in the chosen slot with the selected weapon from the shop
        int randomWeaponIndex = availableWeaponsImages.IndexOf(availableWeaponsImages[slotIndex]);
        WeaponManager.Instance.weaponParentList[slotIndex] = weaponOptions[randomWeaponIndex];

        for (int i = 0; i < currentWeaponsImages.Count; i++)
        {
            if (i == slotIndex)
            {
                currentWeaponsImages[i].sprite = availableSprites[i];
                availableWeaponsImages[i].sprite = currentSprites[i];
            }
            else
            {
                currentWeaponsImages[i].sprite = currentSprites[i];
                availableWeaponsImages[i].sprite = availableSprites[i];
            }
        }

        hasSwappedWeapon = true;
    }

    public void NextScene()
    {
        if (hasSwappedWeapon)
        {
            loadingText.SetActive(true);
            GameManager.Instance.currentRound++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Method to check if a weapon is already in the current weapons list
    private bool IsWeaponInCurrentWeapons(GameObject weapon)
    {
        foreach (Image weaponImage in currentWeaponsImages)
        {
            Weapon currentWeapon =
                WeaponManager.Instance.GetWeaponComponent(
                    WeaponManager.Instance.weaponParentList[currentWeaponsImages.IndexOf(weaponImage)]);
            if (currentWeapon != null && currentWeapon.gameObject == weapon)
            {
                return true;
            }
        }

        return false;
    }
}