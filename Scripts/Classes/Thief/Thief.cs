using Godot;

public partial class Thief : ICharacterClass
{
    public string Name => "Thief";

    public int Level { get; private set; }

    public Stat[] PrimaryStats { get { return new Stat[] { Stat.Dexterity }; } }

}

