using Godot;

public partial class Warrior : ICharacterClass
{
    public string ClassName => GameConstants.CLASS_WARRIOR;

    public Stat[] PrimaryStats { get { return new Stat[] { Stat.Strength }; } }

    public string Description => throw new System.NotImplementedException();

    public int MaxLevel => throw new System.NotImplementedException();

    [Export] public ClassConstraint[] classConstraints { get; set; }
}
