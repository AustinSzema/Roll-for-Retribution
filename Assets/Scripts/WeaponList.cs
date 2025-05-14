using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponList", menuName = "WeaponStuff/WeaponList")]
public class WeaponList : ScriptableObject
{
    public List<WeaponSO> weaponList = new List<WeaponSO>();
}
