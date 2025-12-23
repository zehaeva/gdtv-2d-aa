using Godot;

public partial class Wizard : ICharacterClass
{
    public string Name => "Wizrad";

    public int Level { get; private set; }

    public Stat[] PrimaryStats { get { return new Stat[] { Stat.Intellect }; } }
}
