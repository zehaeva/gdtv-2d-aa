using Godot;

public partial class Warrior : ICharacterClass
{
    public string Name => "Warrior";

    public int Level { get; private set; }

    public Stat[] PrimaryStats { get { return new Stat[] { Stat.Strength }; } }
}
