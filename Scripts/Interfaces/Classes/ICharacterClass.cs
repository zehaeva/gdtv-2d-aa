using Godot;

internal interface ICharacterClass
{
    [Export] public string Name { get; }
    [Export] public int Level { get; }
    [Export] public Stat[] PrimaryStats { get; }
}
