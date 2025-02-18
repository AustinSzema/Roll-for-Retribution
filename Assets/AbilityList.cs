using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityList", menuName = "Scriptable Objects/AbilityList")]
public class AbilityList : ScriptableObject
{
    public List<Ability> _abilities = new List<Ability>();
}
