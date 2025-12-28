using Godot;

public partial class Thief : ICharacterClass
{
    public string ClassName => GameConstants.CLASS_THIEF;

    public Stat[] PrimaryStats { get { return new Stat[] { Stat.Dexterity }; } set { }  }

    public string Description { get; set; }

    public int MaxLevel { get; set; }
}

