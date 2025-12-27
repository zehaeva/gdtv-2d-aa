using Godot;

public partial interface IItem
{
    string ItemName { get; }
    string Description { get; }
    Texture2D Icon { get; }
    bool IsStackable { get; }
}

