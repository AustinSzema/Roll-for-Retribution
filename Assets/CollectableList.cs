using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectableList", menuName = "Scriptable Objects/CollectableList")]
public class CollectableList : ScriptableObject
{
    public List<Collectable> list = new List<Collectable>(5);

}
