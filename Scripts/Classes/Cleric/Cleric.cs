using Godot;

public partial class Cleric : ICharacterClass
{
    public string Name => "Cleric";

    public int Level { get; private set; }

    public Stat[] PrimaryStats { get { return new Stat[] { Stat.Wisdom }; } }
}

