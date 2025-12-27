using Godot;
using System;

public partial class InventorySlot : Control
{
    [Export] public PackedScene InventoryItemScene { get; private set; }
    [Export] public InventoryItem Item { get; private set; }
    [Export] public InventoryItem HintItem { get; private set; } = null;

    [Export] public TextureButton TexturedButton { get; private set; }

    public enum InventorySlotAction { SELECT, SPLIT }

    #region Delegates/Actions/Signals
    public Action<InventorySlot, InventorySlotAction> SlotInput;
    public Action<InventorySlot, bool> SlotHovered;

    public void RaiseSlotInput(InventorySlot which, InventorySlotAction action) => SlotInput?.Invoke(which, action);
    public void RaiseSlotHovered(InventorySlot which, bool isHovering) => SlotHovered?.Invoke(which, isHovering);
    #endregion

    #region Overrides
    public override void _Ready()
    {
        AddToGroup("InventorySlots");

        this.TexturedButton.MouseEntered += TexturedButton_MouseEntered;
        this.TexturedButton.MouseExited += TexturedButton_MouseExited;
        this.TexturedButton.GuiInput += TexturedButton_GuiInput;
    }

    public override void _ExitTree()
    {
        this.TexturedButton.MouseEntered -= TexturedButton_MouseEntered;
        this.TexturedButton.MouseExited -= TexturedButton_MouseExited;
        this.TexturedButton.GuiInput -= TexturedButton_GuiInput;
    }
    #endregion

    #region Handlers
    public void TexturedButton_GuiInput(InputEvent @event)
    {
        InputEventMouseButton a = new InputEventMouseButton();
        if (@event is InputEventMouseButton && @event.IsPressed())
        {
            if (a.ButtonIndex == MouseButton.Left)
            {
                this.RaiseSlotInput(this, InventorySlotAction.SELECT);
            }
            else if (a.ButtonIndex == MouseButton.Right)
            {
                this.RaiseSlotInput(this, InventorySlotAction.SPLIT);
            }
        }

    }

    public void TexturedButton_MouseExited()
    {
        this.RaiseSlotHovered(this, false);
    }

    public void TexturedButton_MouseEntered()
    {
        this.RaiseSlotHovered(this, true);
    }

    #endregion

    #region Hint Items
    public bool IsRespectingHint(InventoryItem newItem, bool inAmountAsWell = true)
    {
        if (this.HintItem == null)
        {
            return true;
        }
        if (inAmountAsWell)
        {
            return (newItem.Name == this.HintItem.ItemName &&
                    newItem.Amount >= this.HintItem.Amount);
        }
        else
        {
            return (newItem.Name == this.HintItem.ItemName);
        }
    }

    public void SetItemHint(InventoryItem newHintItem)
    {
        if (this.HintItem != null)
        {
            this.HintItem.Free();
        }

        this.HintItem = newHintItem;
        this.AddChild(newHintItem);
        this.UpdateSlot();
    }

    public void ClearItemHint()
    {
        if (this.HintItem != null)
        {
            this.HintItem.Free();
        }
        this.HintItem = null;
        this.UpdateSlot();
    }
    #endregion

    #region Selecting and Splitting Items
    // Removes item from the slot and returns it
    public InventoryItem SelectItem()
    {
        Inventory inventory = this.GetParent().GetParent<Inventory>();
        InventoryItem tempItem = this.Item;

        if (tempItem != null)
        {
            tempItem.Reparent(inventory);
            this.Item = null;
            tempItem.ZIndex = 128;
        }

        return tempItem;
    }

    public InventoryItem DeselectItem(InventoryItem inventoryItem)
    {
        if (!IsRespectingHint(inventoryItem))
        {
            return inventoryItem;
        }

        Inventory inventory = this.GetParent().GetParent<Inventory>();

        if (this.IsEmpty())
        {
            inventoryItem.Reparent(this);
            this.Item = inventoryItem;
            this.Item.ZIndex = 64;
            return null;
        }
        else 
        { 
            if (this.HasSameItem(inventoryItem)) // If these are the same type, merge
            {
                this.Item.Amount += inventoryItem.Amount;
                inventoryItem.Free();
                return null;
            }
            else 
            {
                inventoryItem.Reparent(this);
                this.Item.Reparent(inventory);
                InventoryItem tempItem = this.Item;
                this.Item = inventoryItem;
                inventoryItem.ZIndex = 64;
                tempItem.ZIndex = 128;
                return tempItem;
            }
        }
    }

    // Select half of the stack
    public InventoryItem SplitItem()
    {
        if (this.IsEmpty())
        {
            return null;
        }

        Inventory inventory = this.GetParent().GetParent<Inventory>();

        if (this.Item.Amount > 1)
        {
            InventoryItem inventoryItem = new InventoryItem();
            inventoryItem.SetData(Item);
            inventoryItem.Amount = Mathf.FloorToInt(this.Item.Amount / 2);
            this.Item.Amount -= inventoryItem.Amount;
            inventory.AddChild(inventoryItem);
            inventoryItem.ZIndex = 128;
            return inventoryItem;
        }
        else
        {
            return this.Item;
        }
    }
    #endregion

    public void RemoveItem()
    {
        this.RemoveChild(this.Item);
        this.Item.Free();
        this.Item = null;
        this.UpdateSlot();
    }

    public bool IsEmpty()
    { 
        return this.Item == null; 
    }

    public bool HasSameItem(InventoryItem inventoryItem)
    {
        return this.Item == inventoryItem;
    }

    public void SetItem(InventoryItem inventoryItem)
    {
        this.Item = inventoryItem;
    }

    public void UpdateSlot()
    {
        if (Item != null)
        {
            if (!this.GetChildren().Contains(Item))
            {
                this.AddChild(Item);
            }

            if (Item.Amount < 1)
            {
                Item.Fade();
            }
        }
        if (HintItem != null)
        {
            if (!this.GetChildren().Contains(HintItem))
            {
                this.AddChild(HintItem);
            }
            HintItem.Fade();
        }
    }
}
