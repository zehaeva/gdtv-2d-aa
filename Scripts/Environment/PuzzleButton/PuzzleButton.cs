using Godot;
using System;

public partial class PuzzleButton : Area2D
{
    private int bodies_on_top = 0;
    [Export] private AudioStreamPlayer2D AudioStreamPlayer2DNode;
    [Export] private AnimatedSprite2D AnimatedSprite2DNode;

    public Action Pressed;
    public Action UnPressed;
    public void RaisePressed() => Pressed?.Invoke();
    public void RaiseUnPressed() => UnPressed?.Invoke();

    public override void _Ready()
    {
        this.BodyEntered += _on_body_entered;
        this.BodyExited += _on_body_exited;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        this.BodyEntered -= _on_body_entered;
        this.BodyExited -= _on_body_exited;
    }

    protected virtual void _on_body_entered(Node2D body)
    {
        if (body.IsInGroup(GameConstants.GROUP_PUSHABLE) || body is CharacterBody2D)
        {
            bodies_on_top += 1;
            AnimatedSprite2DNode.Play(GameConstants.ANIM_PRESSED);
            AudioStreamPlayer2DNode.PitchScale = 1.0f;
            AudioStreamPlayer2DNode.Play();

            RaisePressed();
        }
    }

    protected virtual void _on_body_exited(Node2D body)
    {
        if (body.IsInGroup(GameConstants.GROUP_PUSHABLE) || body is CharacterBody2D)
        {
            bodies_on_top -= 1;
            if (bodies_on_top == 0)
            {
                AudioStreamPlayer2DNode.PitchScale = 0.8f;
                AudioStreamPlayer2DNode.Play();
                AnimatedSprite2DNode.Play(GameConstants.ANIM_UNPRESSED);

                RaiseUnPressed();
            }
        }
    }
}
