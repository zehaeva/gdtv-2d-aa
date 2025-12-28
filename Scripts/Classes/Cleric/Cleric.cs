using Godot;

public partial class Cleric : ICharacterClass
{
    public string ClassName => GameConstants.CLASS_CLERIC;

    public Stat[] PrimaryStats { get { return new Stat[] { Stat.Wisdom }; } set { } }

    public string Description { get; set; }

    public int MaxLevel { get; set; }
}

