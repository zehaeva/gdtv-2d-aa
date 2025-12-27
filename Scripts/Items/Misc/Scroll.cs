using Godot;

public partial class Scroll : BaseItem, IItem
{
    [Export] public override string ItemName { get { return "Scroll"; } protected set { } }

    [Export] public override string Description { get { return "This is a collectable Scroll"; } protected set { } }

    [Export] public override Texture2D Icon { get; protected set; }

    [Export] public override bool IsStackable { get { return true; } protected set { } }
}
