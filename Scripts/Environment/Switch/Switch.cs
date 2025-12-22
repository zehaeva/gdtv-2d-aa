using Godot;
using System;
using System.Linq;

public partial class Switch : StaticBody2D
{
    [Export] public AudioStreamPlayer2D AudioStreamPlayer2DNode { get; private set; }
    [Export] public AnimatedSprite2D AnimatedSprite2DNode { get; private set; }
    [Export] public Area2D Area2DNode { get; private set; }

    private bool isActivated = false;
    private bool canInteract = false;

    public override void _Ready()
    {
        Area2DNode.AreaEntered += Area2DNode_AreaEntered;
        Area2DNode.AreaExited += Area2DNode_AreaExited;
    }

    public override void _ExitTree()
    {
        Area2DNode.AreaEntered -= Area2DNode_AreaEntered;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_INTERACT) && canInteract)
        {
            AudioStreamPlayer2DNode.Play();

            if (isActivated)
            {
                deactivate_swtich();
            }
            else
            {
                activate_swtich();
            }
        }
    }

    private void Area2DNode_AreaExited(Area2D area)
    {
        canInteract = false;
    }

    private void Area2DNode_AreaEntered(Area2D area)
    {
        canInteract = false;
        Area2D a = Area2DNode.GetOverlappingAreas().FirstOrDefault();
        if (a is not null && a.GetOwner<Player>() is not null)
        { 
            canInteract = true;
        }
    }

    private void activate_swtich()
    {
        AnimatedSprite2DNode.Play(GameConstants.ANIM_ACTIVATED);

        isActivated = true;

        GameEvents.RaiseSwitchActivated();//switch_activated.emit();
    }

    private void deactivate_swtich()
    {
        AnimatedSprite2DNode.Play(GameConstants.ANIM_DEACTIVATED);

        isActivated = false;

        GameEvents.RaiseSwitchDectivated();//switch_deactivated.emit();
    }
}