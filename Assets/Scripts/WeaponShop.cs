using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    // Add this to track the last swapped weapon slot
    private int lastSwappedSlot = -1;

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
        PlayerController player = FindObjectOfType<PlayerController>();
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player.GetComponent<Collider>().enabled = false;
        internalWeaponsList.Clear();
        internalWeaponsList.AddRange(weaponList.weaponList);

        // Update current weapons displayed
        for (int i = 0; i < currentWeaponsImages.Count; i++)
        {
            var weapon = WeaponManager.Instance.GetWeaponComponent(WeaponManager.Instance.weaponParentList[i]);
            currentWeaponsImages[i].sprite = weapon.weaponUISprite;
            currentDescriptions[i].text = weapon.weaponDescription;
        }

        // Initialize available weapons for swapping
        weaponOptions.Clear();
        for (int i = 0; i < availableWeaponsImages.Count; i++)
        {
            GameObject selectedWeapon = null;
            Weapon weapon = null;

            do
            {
                int random = Random.Range(0, internalWeaponsList.Count);
                selectedWeapon = internalWeaponsList[random];
                weapon = WeaponManager.Instance.GetWeaponComponent(selectedWeapon);
            } while (IsWeaponInCurrentWeapons(selectedWeapon));

            weaponOptions.Add(selectedWeapon);
            availableWeaponsImages[i].sprite = weapon.weaponUISprite;
            availableWeaponsDescriptions[i].text = weapon.weaponDescription;
            arrowDescriptions[i].text = "Replace " + WeaponManager.Instance
                                            .GetWeaponComponent(WeaponManager.Instance.weaponParentList[i]).weaponName +
                                        " with " + weapon.weaponName;
            internalWeaponsList.Remove(selectedWeapon);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void SwapToNewWeapon(int slotIndex)
    {
    
        if (slotIndex < 0 || slotIndex >= currentWeaponsImages.Count) return;

        // Check if there's an existing swap and reset it if necessary
        if (lastSwappedSlot != -1 && lastSwappedSlot != slotIndex)
        {
            // Reset the previous swap
            ResetSwap(lastSwappedSlot);
        }

        // Perform the new swap
        lastSwappedSlot = slotIndex;

        // Swap the top weapon with the bottom weapon for the given index
        var topWeapon = WeaponManager.Instance.GetWeaponComponent(WeaponManager.Instance.weaponParentList[slotIndex]);
        var bottomWeapon = WeaponManager.Instance.GetWeaponComponent(weaponOptions[slotIndex]);

        // Swap images and descriptions for the selected top-bottom pair
        currentWeaponsImages[slotIndex].sprite = bottomWeapon.weaponUISprite;
        currentDescriptions[slotIndex].text = bottomWeapon.weaponDescription;

        availableWeaponsImages[slotIndex].sprite = topWeapon.weaponUISprite;
        availableWeaponsDescriptions[slotIndex].text = topWeapon.weaponDescription;

        // Update the arrow description to reflect the swap
        arrowDescriptions[slotIndex].text = "Replace " + bottomWeapon.weaponName + " with " + topWeapon.weaponName;

        hasSwappedWeapon = true;
        


    }

    private void ResetSwap(int slotIndex)
    {
        // Restore original weapon images and descriptions
        var originalTopWeapon = WeaponManager.Instance.GetWeaponComponent(WeaponManager.Instance.weaponParentList[slotIndex]);
        var originalBottomWeapon = WeaponManager.Instance.GetWeaponComponent(weaponOptions[slotIndex]);

        currentWeaponsImages[slotIndex].sprite = originalTopWeapon.weaponUISprite;
        currentDescriptions[slotIndex].text = originalTopWeapon.weaponDescription;

        availableWeaponsImages[slotIndex].sprite = originalBottomWeapon.weaponUISprite;
        availableWeaponsDescriptions[slotIndex].text = originalBottomWeapon.weaponDescription;

        // Reset arrow description
        arrowDescriptions[slotIndex].text = "Replace " + originalTopWeapon.weaponName + " with " + originalBottomWeapon.weaponName;
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
