using Godot;

public partial class Warrior : CharacterClass, ICharacterClass
{
    public override string ClassName => GameConstants.CLASS_WARRIOR;

    public override Stat[] PrimaryStats { get { return new Stat[] { Stat.Strength }; } set { } }

    public override string Description { get; set; }

    public override int MaxLevel { get; set; }

    [Export] public ClassConstraint[] classConstraints { get; set; }
}
