using Godot;
using System.Linq;

public abstract partial class EnvironmentInteractable : StaticBody2D
{
    [Export] public Area2D InteractableTriggerArea { get; private set; }

    protected bool canInteract = false;

    public override void _Ready()
    {
        InteractableTriggerArea.AreaEntered += Area2DNode_AreaEntered;
        InteractableTriggerArea.AreaExited += Area2DNode_AreaExited;
    }

    public override void _ExitTree()
    {
        InteractableTriggerArea.AreaEntered -= Area2DNode_AreaEntered;
        InteractableTriggerArea.AreaExited -= Area2DNode_AreaExited;
    }

    protected void Area2DNode_AreaExited(Area2D area)
    {
        canInteract = false;
        GD.Print("END INTERACT!");
    }

    protected void Area2DNode_AreaEntered(Area2D area)
    {
        canInteract = false;
        Area2D a = InteractableTriggerArea.GetOverlappingAreas().FirstOrDefault();
        if (a is not null && a.GetOwner<Player>() is not null)
        {
            GD.Print("CAN INTERACT!");
            canInteract = true;
        }
    }
}
