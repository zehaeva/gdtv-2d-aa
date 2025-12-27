using Godot;

public partial class Sword : Node, IItem
{
    [Export] public string Description { get; private set; }

    [Export] public Texture2D Icon { get; private set; }

    [Export] public string ItemName { get; private set; }

    [Export] public bool IsStackable { get; private set; }
}
