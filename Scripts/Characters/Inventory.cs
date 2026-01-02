using Godot;
using System.Collections.Generic;

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
}
