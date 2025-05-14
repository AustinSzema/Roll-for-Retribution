using UnityEngine;

public class ShopItemSO : ScriptableObject
{
public enum ItemType{

Eyeball,
Scroll,
Weapon

}

public ItemType itemType;

public GameObject itemPrefab;

public int cost = 1;

}
