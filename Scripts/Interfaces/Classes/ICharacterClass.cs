using Godot;

public interface ICharacterClass
{
    [Export] public string ClassName { get; }
    [Export] public string Description { get; protected set; }
    [Export] public Stat[] PrimaryStats { get; protected set; }
    [Export] public int MaxLevel { get; protected set; }

    public bool CheckForLevelUp(Character character);
}
