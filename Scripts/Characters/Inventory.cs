using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class Inventory : Resource
{
    public List<InventoryItem> InventoryItems { get; protected set; }  = new List<InventoryItem>();

    public void AddItem(InventoryItem item)
    {
        InventoryItems.Add(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        InventoryItems.Remove(item);
    }

    public bool HasItem(InventoryItem item)
    {
        return InventoryItems.Contains(item);
    }

    public bool HasItem(InventoryItem item, int amount)
    {
        return InventoryItems.
                    Where(invItem => invItem.ItemName == item.ItemName && invItem.Amount >= amount).
                    FirstOrDefault() != null;
    }   
}
