using Godot;
using System;
using System.Linq;

public partial class Switch : EnvironmentInteractable
{
    [Export] public AudioStreamPlayer2D AudioStreamPlayer2DNode { get; private set; }
    [Export] public AnimatedSprite2D AnimatedSprite2DNode { get; private set; }
    [Export(PropertyHint.Range, "-20, 20, 1")] public int SwitchValue { get; private set; } = 1;

    public Action<int> Activated;
    public Action<int> Deactivated;
    public void RaiseActivated(int value) => Activated?.Invoke(value);
    public void RaiseDeactivated(int value) => Deactivated?.Invoke(value);

    protected bool isActivated = false;

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

    protected void activate_swtich()
    {
        AnimatedSprite2DNode.Play(GameConstants.ANIM_ACTIVATED);

        isActivated = true;

        GameEvents.RaiseSwitchActivated();
        RaiseActivated(SwitchValue);
    }

    protected void deactivate_swtich()
    {
        AnimatedSprite2DNode.Play(GameConstants.ANIM_DEACTIVATED);

        isActivated = false;

        GameEvents.RaiseSwitchDectivated();
        RaiseDeactivated(SwitchValue);
    }
}