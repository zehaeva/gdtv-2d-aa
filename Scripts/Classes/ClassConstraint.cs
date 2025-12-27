using Godot;

[GlobalClass]
public partial class ClassConstraint : Resource
{
    [Export] public ResourceConstraint Constraint { get; set; }
}
