using System.Collections.Generic;

public static class PlayerInventory
{	public static int money = 0;
	public static List<ShopItemSO> eyeballs = new List<ShopItemSO>();
	public static List<ShopItemSO> scrolls = new List<ShopItemSO>();
	public static List<ShopItemSO> weapons = new List<ShopItemSO>();
	public static void AddItem(ShopItemSO itemSO){
		switch(itemSO.itemType){
			case ShopItemSO.ItemType.Eyeball:
				eyeballs.Add(itemSO);
				break;
			case ShopItemSO.ItemType.Scroll:
				scrolls.Add(itemSO);
				break;
			case ShopItemSO.ItemType.Weapon:
				weapons.Add(itemSO);
				break;
		}
		RefreshInventory();
	}
	public static void RefreshInventory(){
		/*foreach(in eyeballs){
			
		}*/
		// update the visuals for the player inventory
	}
}