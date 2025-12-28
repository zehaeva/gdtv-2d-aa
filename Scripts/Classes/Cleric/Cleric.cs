using Godot;

public partial class Cleric : CharacterClass, ICharacterClass
{
    public override string ClassName => GameConstants.CLASS_CLERIC;

    public override Stat[] PrimaryStats { get { return new Stat[] { Stat.Wisdom }; } set { } }

    public override string Description { get; set; }

    public override int MaxLevel { get; set; }
}

