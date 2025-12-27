using Godot;


public abstract partial class ResourceConstraint : Resource
{
    [Export] public GameResource Resource { get; set; }
}
