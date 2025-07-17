using UnityEngine;
using Magic.Inventory;

[System.Serializable]
public class InventoryQuantity
{
    public Item item;
    public int quantity;

    public InventoryQuantity(Item item, int quantity = 1)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
