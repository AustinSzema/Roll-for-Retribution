using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponManager : MonoBehaviour
{
    [HideInInspector] public List<GameObject> weaponParentList = new List<GameObject>();

    [HideInInspector] public static List<Weapon> weapons = new List<Weapon>();
    public static WeaponManager Instance { get; private set; }

    [SerializeField] private WeaponList mainWeaponList;
    [SerializeField] private WeaponList startingWeaponList;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        foreach (WeaponSO weapon in startingWeaponList.weaponList)
        {
            PlayerInventory.AddItem(weapon);
        }
        foreach (ShopItemSO weapon in PlayerInventory.weapons)
        {
            Vector3 randomOffset = Random.onUnitSphere * 20f;
            Instantiate(weapon.itemPrefab, transform.position + randomOffset, Quaternion.identity);
        }
        weapons = FindObjectsByType<Weapon>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
    }

    public Weapon GetWeaponComponent(GameObject obj)
    {
        return obj.GetComponent<Weapon>() ?? obj.GetComponentInChildren<Weapon>();
    }
}
