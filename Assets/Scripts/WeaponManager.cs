using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [HideInInspector] public List<GameObject> weaponParentList = new List<GameObject>();
    public static WeaponManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public Weapon GetWeaponComponent(GameObject obj)
    {
        return obj.GetComponent<Weapon>() ?? obj.GetComponentInChildren<Weapon>();
    }
}
