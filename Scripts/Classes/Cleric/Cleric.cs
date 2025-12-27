using Godot;

public partial class Cleric : ICharacterClass
{
    public string ClassName => GameConstants.CLASS_CLERIC;

    public Stat[] PrimaryStats { get { return new Stat[] { Stat.Wisdom }; } }

    public string Description => throw new System.NotImplementedException();

    public int MaxLevel => throw new System.NotImplementedException();
}

