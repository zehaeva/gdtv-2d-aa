using Godot;
using Godot.Collections;
using System.Linq;

[GlobalClass]
public partial class Wizard : CharacterClass, ICharacterClass
{
    public override string ClassName => GameConstants.CLASS_WIZARD;

    public override Stat[] PrimaryStats { get { return new Stat[] { Stat.Intellect }; } }

    [Export] public ClassConstraint[] ClassConstraints { get; set; }
}
