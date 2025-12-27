using Godot;

public abstract partial class BaseItem : Node2D, IItem
{
    public abstract string ItemName { get; protected set; }
    public abstract string Description { get; protected set; }
    public abstract Texture2D Icon { get; protected set; }
    public abstract bool IsStackable { get; protected set; }

    public override void _Ready()
    {
        AddToGroup("items");
    }
}
