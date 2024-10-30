using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] private WeaponList everyWeaponList;
    [SerializeField] private GameObject loadingText;
    [SerializeField] private List<Image> currentWeaponsImages = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> currentDescriptions = new List<TextMeshProUGUI>();
    [SerializeField] private List<Image> availableWeaponsImages = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> availableWeaponsDescriptions = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> arrowDescriptions = new List<TextMeshProUGUI>();

    private List<GameObject> internalWeaponsList = new List<GameObject>();
    private List<GameObject> weaponOptions = new List<GameObject>();
    private List<GameObject> oldWeapons = new List<GameObject>();
    private List<GameObject> newWeapons = new List<GameObject>();

    // Store the initial weapon list state
    private List<GameObject> originalWeaponParentList = new List<GameObject>();

    private int lastSwappedSlot = -1;
    private bool hasSwappedWeapon = false;

    private void OnEnable()
    {
        GameManager.Instance.gameIsPaused = true;
        PlayerController player = FindObjectOfType<PlayerController>();
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player.GetComponent<Collider>().enabled = false;

        InitializeWeaponLists();
        DisplayCurrentWeapons();
        InitializeAvailableWeapons();

        // Store the initial state of weaponParentList for resetting purposes
        originalWeaponParentList = new List<GameObject>(WeaponManager.Instance.weaponParentList);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.shopActive = true;
    }

    private void InitializeWeaponLists()
    {
        internalWeaponsList.Clear();
        internalWeaponsList.AddRange(everyWeaponList.weaponList);
    }

    private void DisplayCurrentWeapons()
    {
        for (int i = 0; i < currentWeaponsImages.Count; i++)
        {
            var weapon = WeaponManager.Instance.GetWeaponComponent(WeaponManager.Instance.weaponParentList[i]);
            currentWeaponsImages[i].sprite = weapon.weaponUISprite;
            currentDescriptions[i].text = weapon.weaponDescription;
        }
        oldWeapons = new List<GameObject>(WeaponManager.Instance.weaponParentList);
    }

    private void InitializeAvailableWeapons()
    {
        weaponOptions.Clear();
        newWeapons.Clear();

        for (int i = 0; i < availableWeaponsImages.Count; i++)
        {
            GameObject selectedWeaponObject = null;
            Weapon weapon = null;

            do
            {
                int random = Random.Range(0, internalWeaponsList.Count);
                selectedWeaponObject = internalWeaponsList[random];
                weapon = WeaponManager.Instance.GetWeaponComponent(selectedWeaponObject);
            } while (IsWeaponInCurrentWeapons(selectedWeaponObject));

            weaponOptions.Add(selectedWeaponObject);
            availableWeaponsImages[i].sprite = weapon.weaponUISprite;
            availableWeaponsDescriptions[i].text = weapon.weaponDescription;
            arrowDescriptions[i].text = $"Replace {WeaponManager.Instance.GetWeaponComponent(WeaponManager.Instance.weaponParentList[i]).weaponName} with {weapon.weaponName}";

            internalWeaponsList.Remove(selectedWeaponObject);
            newWeapons.Add(selectedWeaponObject);
        }
    }

    public void SwapToNewWeapon(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= currentWeaponsImages.Count) return;

        // Reset all slots to original state except for the selected slot
        ResetAllExcept(slotIndex);

        var currentWeapon = WeaponManager.Instance.GetWeaponComponent(WeaponManager.Instance.weaponParentList[slotIndex]);
        var newWeapon = WeaponManager.Instance.GetWeaponComponent(weaponOptions[slotIndex]);

        if (currentWeapon.gameObject == newWeapon.gameObject)
        {
            ResetSwap(slotIndex);
            return;
        }

        lastSwappedSlot = slotIndex;
        PerformSwap(slotIndex, currentWeapon, newWeapon);
    }

    private void ResetAllExcept(int selectedIndex)
    {
        for (int i = 0; i < currentWeaponsImages.Count; i++)
        {
            if (i != selectedIndex)
            {
                // Reset the weaponParentList to the initial state for all slots except the selected one
                WeaponManager.Instance.weaponParentList[i] = originalWeaponParentList[i];

                // Update images and descriptions for current and available weapons
                var originalWeapon = WeaponManager.Instance.GetWeaponComponent(originalWeaponParentList[i]);
                currentWeaponsImages[i].sprite = originalWeapon.weaponUISprite;
                currentDescriptions[i].text = originalWeapon.weaponDescription;

                var availableWeapon = WeaponManager.Instance.GetWeaponComponent(weaponOptions[i]);
                availableWeaponsImages[i].sprite = availableWeapon.weaponUISprite;
                availableWeaponsDescriptions[i].text = availableWeapon.weaponDescription;
                arrowDescriptions[i].text = $"Replace {originalWeapon.weaponName} with {availableWeapon.weaponName}";
            }
        }
    }

    private void PerformSwap(int slotIndex, Weapon currentWeapon, Weapon newWeapon)
    {
        currentWeaponsImages[slotIndex].sprite = newWeapon.weaponUISprite;
        currentDescriptions[slotIndex].text = newWeapon.weaponDescription;
        availableWeaponsImages[slotIndex].sprite = currentWeapon.weaponUISprite;
        availableWeaponsDescriptions[slotIndex].text = currentWeapon.weaponDescription;
        arrowDescriptions[slotIndex].text = $"Replace {newWeapon.weaponName} with {currentWeapon.weaponName}";

        WeaponManager.Instance.weaponParentList[slotIndex] = newWeapons[slotIndex];
        newWeapons[slotIndex] = oldWeapons[slotIndex];


        hasSwappedWeapon = true;
    }

    private void ResetSwap(int slotIndex)
    {
        var originalWeapon = WeaponManager.Instance.GetWeaponComponent(WeaponManager.Instance.weaponParentList[slotIndex]);
        var newWeapon = WeaponManager.Instance.GetWeaponComponent(weaponOptions[slotIndex]);

        currentWeaponsImages[slotIndex].sprite = originalWeapon.weaponUISprite;
        currentDescriptions[slotIndex].text = originalWeapon.weaponDescription;
        availableWeaponsImages[slotIndex].sprite = newWeapon.weaponUISprite;
        availableWeaponsDescriptions[slotIndex].text = newWeapon.weaponDescription;
        arrowDescriptions[slotIndex].text = $"Replace {originalWeapon.weaponName} with {newWeapon.weaponName}";
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

    private bool IsWeaponInCurrentWeapons(GameObject weapon)
    {
        foreach (var weaponImage in currentWeaponsImages)
        {
            var currentWeapon = WeaponManager.Instance.GetWeaponComponent(WeaponManager.Instance.weaponParentList[currentWeaponsImages.IndexOf(weaponImage)]);
            if (currentWeapon != null && currentWeapon.gameObject == weapon)
            {
                return true;
            }
        }
        return false;
    }
}
