using Godot;
using System;

public partial class ItemSlot : PanelContainer
{
    private TextureRect _textureRect;

    public override void _Ready()
    {
        _textureRect = GetNode<TextureRect>("%TextureRect");
    }

    public void Display(InventoryItem item)
    {
        GD.Print("Display: " + item.Description);
        _textureRect.Texture = item.Icon;
    }
}
