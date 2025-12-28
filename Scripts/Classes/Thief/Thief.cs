using Godot;

public partial class Thief : CharacterClass, ICharacterClass
{
    public override string ClassName => GameConstants.CLASS_THIEF;

    public override Stat[] PrimaryStats { get { return new Stat[] { Stat.Dexterity }; } set { }  }

    public override string Description { get; set; }

    public override int MaxLevel { get; set; }
}

