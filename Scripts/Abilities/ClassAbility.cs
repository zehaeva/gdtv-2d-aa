using Godot;

[GlobalClass]
public partial class ClassAbility : Resource
{
    [Export] public int ClassLevel { get; set; }
    [Export] public Ability Ability { get; set; }
}
