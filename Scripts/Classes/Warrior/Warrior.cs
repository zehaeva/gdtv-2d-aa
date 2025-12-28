using Godot;

public partial class Warrior : ICharacterClass
{
    public string ClassName => GameConstants.CLASS_WARRIOR;

    public Stat[] PrimaryStats { get { return new Stat[] { Stat.Strength }; } set { } }

    public string Description { get; set; }

    public int MaxLevel { get; set; }

    [Export] public ClassConstraint[] classConstraints { get; set; }
}
