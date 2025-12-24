using Godot;

public partial class Sword : Node, IItem
{
    [Export] public string Description { get; private set; }

    [Export] public Sprite2D Icon { get; private set; }
}
