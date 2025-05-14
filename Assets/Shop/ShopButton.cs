using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private ShopItemSO item;

    public void OnClick()
    {
        if (PlayerInventory.money > item.cost && PlayerInventory.money - item.cost >= 0)
        {
            PlayerInventory.money -= item.cost;
            PlayerInventory.AddItem(item);
            gameObject.SetActive(false);
        }

    }
}