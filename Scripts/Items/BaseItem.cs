using Godot;

[GlobalClass]
public partial class BaseItem : Resource, IItem
{
    [Export] public string ItemName { get; protected set; }
    [Export] public string Description { get; protected set; }
    [Export] public PackedScene Scene { get; protected set; }
    [Export] public Texture2D Icon { get; protected set; }
    [Export] public bool IsStackable { get; protected set; }

    public string Name { get; set; }
}
