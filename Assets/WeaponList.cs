using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponList", menuName = "WeaponStuff/WeaponList")]
public class WeaponList : ScriptableObject
{
    public List<GameObject> weaponList = new List<GameObject>();
}
