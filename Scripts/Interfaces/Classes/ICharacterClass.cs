using Godot;

public interface ICharacterClass
{
    [Export] public string ClassName { get; }
    [Export] public string Description { get; }
    [Export] public Stat[] PrimaryStats { get; }
    [Export] public int MaxLevel { get; }
}
