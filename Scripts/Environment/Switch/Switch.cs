using Godot;
using System;
using System.Linq;

public partial class Switch : EnvironmentInteractable
{
    [Export] public AudioStreamPlayer2D AudioStreamPlayer2DNode { get; private set; }
    [Export] public AnimatedSprite2D AnimatedSprite2DNode { get; private set; }

    private bool isActivated = false;

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

    private void activate_swtich()
    {
        AnimatedSprite2DNode.Play(GameConstants.ANIM_ACTIVATED);

        isActivated = true;

        GameEvents.RaiseSwitchActivated();
    }

    private void deactivate_swtich()
    {
        AnimatedSprite2DNode.Play(GameConstants.ANIM_DEACTIVATED);

        isActivated = false;

        GameEvents.RaiseSwitchDectivated();
    }
}