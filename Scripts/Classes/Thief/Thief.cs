using Godot;

public partial class Thief : ICharacterClass
{
    public string ClassName => GameConstants.CLASS_THIEF;

    public Stat[] PrimaryStats { get { return new Stat[] { Stat.Dexterity }; } }

    public string Description => throw new System.NotImplementedException();

    public int MaxLevel => throw new System.NotImplementedException();
}

