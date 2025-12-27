using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class Inventory : Control
{
    [Export] public int Rows { get; set; } = 3;
    [Export] public int Cols { get; set; } = 5;

    [Export] public GridContainer InventoryGrid { get; set; }
    [Export] public PackedScene InventorySlotScene { get; set; }
    public InventorySlot[] Slots { get; set; }

    [Export] public ToolTip tooltip { get; set; }

    public static InventoryItem SelectedItem { get; set; } = null;

    #region Overrides
    public override void _Ready()
    {
        this.Slots = new InventorySlot[Cols * Rows];
        InventorySlotScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/Inventory/inventory_slot.tscn");
        InventoryGrid.Columns = this.Cols;
        for (int i = 0; i < (this.Cols * this.Rows); i++)
        {
            InventorySlot slot = InventorySlotScene.Instantiate() as InventorySlot;
            this.Slots.Append(slot);
            InventoryGrid.AddChild(slot);
            slot.SlotInput += OnSlotInput;
            slot.SlotHovered += OnSlotHovered;
        }
        tooltip.Visible = false;
    }

    public override void _Process(double delta)
    {
        if (tooltip != null)
        {
            tooltip.GlobalPosition = GetGlobalMousePosition() + Vector2.One * 8;

            if (SelectedItem != null)
            {
                tooltip.Visible = false;
                SelectedItem.GlobalPosition = GetGlobalMousePosition();
            }
        }
    }
    #endregion

    #region Handlers
    private void OnSlotInput(InventorySlot slot, InventorySlot.InventorySlotAction slotAction)
    {
        if (SelectedItem == null)
        {
            switch (slotAction)
            {
                case InventorySlot.InventorySlotAction.SELECT:
                    SelectedItem = slot.SelectItem();
                    break;
                case InventorySlot.InventorySlotAction.SPLIT:
                    SelectedItem = slot.SplitItem();
                    break;
            }
        }
        else
        {
            SelectedItem = slot.DeselectItem(SelectedItem);
        }
    }
    private void OnSlotHovered(InventorySlot slot, bool hovering)
    {
        if(slot.Item !=  null)
        {
            tooltip.SetText(slot.Item.Name);
            tooltip.Visible = hovering;
        }
        else if (slot.HintItem != null)
        {
            tooltip.SetText(slot.HintItem.Name);
            tooltip.Visible = hovering;
        }
    }
    #endregion

    public void AddItem(InventoryItem item, int amount)
    {
        InventoryItem inventoryItem = InventorySlotScene.Instantiate<InventoryItem>();

        inventoryItem.SetData(item);

        item.QueueFree();

        if (item.IsStackable)
        {
            foreach(InventorySlot slot in Slots)
            {
                if (slot.Item != null && slot.Item.ItemName == inventoryItem.Item.Name)
                {
                    slot.Item.Amount += inventoryItem.Amount;
                    return;
                }
            }
        }
        foreach (InventorySlot slot in Slots)
        {
            if (slot.Item != null && slot.IsRespectingHint(inventoryItem))
            {
                slot.SetItem(inventoryItem);
                slot.UpdateSlot();
                return;
            }
        }
    }

    public IItem RetrieveItem(string itemName)
    {
        return null;
    }

    public IItem[] AllItems()
    {
        List<InventoryItem> bi = new List<InventoryItem>();

        foreach(InventorySlot slot in Slots)
        {
            bi.Add(slot.Item);
        }

        return bi.ToArray();
    }

    public IItem[] All(string itemName)
    {
        List<InventoryItem> bi = new List<InventoryItem>();

        foreach (InventorySlot slot in Slots)
        {
            if (slot.Item != null && slot.Item.ItemName == itemName)
            {
                bi.Add(slot.Item);
            }
        }

        return bi.ToArray();
    }

    public void RemoveAll(string itemName)
    {
        foreach (InventorySlot slot in Slots)
        {
            if (slot.Item != null && slot.Item.ItemName == itemName)
                slot.RemoveItem();
        }
    }

    public void ClearInventory()
    {
        foreach (InventorySlot slot in Slots)
        {
            slot.RemoveItem();
        }
    }
}
