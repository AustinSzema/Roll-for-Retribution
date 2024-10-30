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

    // Add this to track if a weapon has already been swapped this round
    private bool hasSwappedThisRound = false;

    [FormerlySerializedAs("weaponList")] [SerializeField] private WeaponList everyWeaponList;
    
    
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
    
    
    private List<GameObject> oldWeapons = new List<GameObject>();
    private List<GameObject> oldCopyWeapons = new List<GameObject>();
    private List<GameObject> newWeapons = new List<GameObject>();
    private List<GameObject> newCopyWeapons = new List<GameObject>();


    private List<bool> flipped = new List<bool>();
    
    
    

    private bool hasSwappedWeapon = false;

    private void OnEnable()
    {
        GameManager.Instance.gameIsPaused = true;
        PlayerController player = FindObjectOfType<PlayerController>();
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player.GetComponent<Collider>().enabled = false;
        internalWeaponsList.Clear();
        internalWeaponsList.AddRange(everyWeaponList.weaponList);

        // Update current weapons displayed
        for (int i = 0; i < currentWeaponsImages.Count; i++)
        {
            var weapon = WeaponManager.Instance.GetWeaponComponent(WeaponManager.Instance.weaponParentList[i]);
            currentWeaponsImages[i].sprite = weapon.weaponUISprite;
            currentDescriptions[i].text = weapon.weaponDescription;
            oldWeapons = WeaponManager.Instance.weaponParentList;
            oldCopyWeapons = WeaponManager.Instance.weaponParentList;
            flipped.Add(false);

        }

        // Initialize available weapons for swapping
        weaponOptions.Clear();
        for (int i = 0; i < availableWeaponsImages.Count; i++)
        {
            GameObject selectedWeaponObject = null;
            Weapon weapon = null;

                int random = Random.Range(0, internalWeaponsList.Count);
                selectedWeaponObject = internalWeaponsList[random];
                newWeapons.Add(selectedWeaponObject);
                newCopyWeapons.Add(selectedWeaponObject);
                weapon = WeaponManager.Instance.GetWeaponComponent(selectedWeaponObject);

            
            
            weaponOptions.Add(selectedWeaponObject);
            availableWeaponsImages[i].sprite = weapon.weaponUISprite;
            availableWeaponsDescriptions[i].text = weapon.weaponDescription;
            arrowDescriptions[i].text = "Replace " + WeaponManager.Instance
                                            .GetWeaponComponent(WeaponManager.Instance.weaponParentList[i]).weaponName +
                                        " with " + weapon.weaponName;
            internalWeaponsList.Remove(selectedWeaponObject);
        }
        

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        GameManager.Instance.shopActive = true;
        
        // Reset swap tracker for new round
        hasSwappedThisRound = false; // Reset swap state when the shop opens
    }

    public void SwapToNewWeapon(int slotIndex)
    {
        for (int i = 0; i < currentWeaponsImages.Count; i++)
        {
            if (i == slotIndex)
            {
                flipped[slotIndex] = !flipped[slotIndex];

                if (flipped[slotIndex])
                {
                    currentWeaponsImages[slotIndex].sprite = WeaponManager.Instance.GetWeaponComponent(oldWeapons[slotIndex]).weaponUISprite;
                    currentDescriptions[slotIndex].text =
                        WeaponManager.Instance.GetWeaponComponent(oldCopyWeapons[slotIndex]).weaponDescription;

                    availableWeaponsImages[slotIndex].sprite = WeaponManager.Instance.GetWeaponComponent(newCopyWeapons[slotIndex]).weaponUISprite;
                    availableWeaponsDescriptions[slotIndex].text = 
                        WeaponManager.Instance.GetWeaponComponent(newCopyWeapons[slotIndex]).weaponDescription;
                }
                else
                {
                    currentWeaponsImages[slotIndex].sprite =  WeaponManager.Instance.GetWeaponComponent(newCopyWeapons[slotIndex]).weaponUISprite;
                    currentDescriptions[slotIndex].text =
                        WeaponManager.Instance.GetWeaponComponent(newCopyWeapons[slotIndex]).weaponDescription;

                    availableWeaponsImages[slotIndex].sprite = WeaponManager.Instance.GetWeaponComponent(oldWeapons[slotIndex]).weaponUISprite;
                    availableWeaponsDescriptions[slotIndex].text = 
                        WeaponManager.Instance.GetWeaponComponent(oldCopyWeapons[slotIndex]).weaponDescription;
                }
            }
            else
            {
                currentWeaponsImages[i].sprite = WeaponManager.Instance.GetWeaponComponent(oldWeapons[i]).weaponUISprite;
                currentDescriptions[i].text =
                    WeaponManager.Instance.GetWeaponComponent(oldCopyWeapons[i]).weaponDescription;

                availableWeaponsImages[i].sprite = WeaponManager.Instance.GetWeaponComponent(newCopyWeapons[i]).weaponUISprite;
                availableWeaponsDescriptions[i].text = 
                    WeaponManager.Instance.GetWeaponComponent(newCopyWeapons[i]).weaponDescription;
            }

        }
        

        // if (slotIndex < 0 || slotIndex >= currentWeaponsImages.Count) return;
        //
        // // Check if there's an existing swap and reset it if necessary
        // if (lastSwappedSlot != -1 && lastSwappedSlot != slotIndex)
        // {
        //     // Reset the previous swap before performing the new swap
        //     ResetSwap(lastSwappedSlot);
        // }
        //
        // // Get references for current and new weapons
        // var currentWeapon = WeaponManager.Instance.GetWeaponComponent(WeaponManager.Instance.weaponParentList[slotIndex]);
        // var newWeapon = WeaponManager.Instance.GetWeaponComponent(weaponOptions[slotIndex]);
        //
        // // Check if we are trying to swap to the same weapon
        // if (currentWeapon.gameObject == newWeapon.gameObject)
        // {
        //     // If the same weapon is clicked, reset it
        //     ResetSwap(slotIndex);
        //     return; // Exit the method
        // }
        //
        // // Perform the swap
        // lastSwappedSlot = slotIndex;
        //
        // // Update the current weapon images and descriptions
        // currentWeaponsImages[slotIndex].sprite = newWeapon.weaponUISprite;
        // currentDescriptions[slotIndex].text = newWeapon.weaponDescription;
        //
        // // Update the available weapon images and descriptions
        // availableWeaponsImages[slotIndex].sprite = currentWeapon.weaponUISprite;
        // availableWeaponsDescriptions[slotIndex].text = currentWeapon.weaponDescription;
        //
        // // Update the arrow description to reflect the swap
        // arrowDescriptions[slotIndex].text = "Replace " + newWeapon.weaponName + " with " + currentWeapon.weaponName;
        //
        // for (int i = 0; i < WeaponManager.Instance.weaponParentList.Count; i++)
        // {
        //     if(i == slotIndex){
        //         // Update the weaponParentList to point to the new weapon
        //         WeaponManager.Instance.weaponParentList[slotIndex] = newCopyWeapons[slotIndex];
        //         newWeapons[slotIndex] = oldCopyWeapons[slotIndex];
        //     }
        //     else
        //     {
        //         WeaponManager.Instance.weaponParentList[slotIndex] = oldCopyWeapons[slotIndex];
        //         newWeapons[slotIndex] = newCopyWeapons[slotIndex];
        //     }
        // }
        //
        //
        // hasSwappedThisRound = true; // Mark that a swap has occurred
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
