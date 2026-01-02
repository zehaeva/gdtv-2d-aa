using Godot;
using System;

public partial class InventoryDialog : PanelContainer
{
    private Button _closeButton;
    private GridContainer _inventoryGrid;

    [Export] public PackedScene SlotScene { get; private set; }

    public bool IsOpen { get; private set; } = false;

    #region overrides
    public override void _Ready()
    {
        _closeButton = GetNode<Button>("%CloseButton");
        _closeButton.Pressed += HandleClosedButtonPressed;

        _inventoryGrid = GetNode<GridContainer>("%GridContainer");
    }

    public override void _ExitTree()
    {
        _closeButton.Pressed -= HandleClosedButtonPressed;
    }
    #endregion

    private void HandleClosedButtonPressed()
    {
        IsOpen = false;
        ClearGrid();
        Hide();
    }

    private void ClearGrid()
    {
        foreach (Node node in _inventoryGrid.GetChildren())
        {
            _inventoryGrid.RemoveChild(node);
        }
    }

    public void Open(Inventory inventory)
    {
        Show();
        IsOpen = true;
        ClearGrid();
        GD.Print("inventory: " + inventory.InventoryItems.Count);
        foreach (InventoryItem item in inventory.InventoryItems)
        {
            GD.Print("Open: " + item.Description);
            ItemSlot slot = SlotScene.Instantiate<ItemSlot>();
            _inventoryGrid.AddChild(slot);

            slot.Display(item);
        }
    }
}
