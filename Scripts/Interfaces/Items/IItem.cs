using Godot;

public partial interface IItem
{
    //[Export] string Name { get; }
    string Description { get; }
    Sprite2D Icon { get; }

}

