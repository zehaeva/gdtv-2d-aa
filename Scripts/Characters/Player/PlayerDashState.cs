using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimerNode;
    [Export] private Timer cooldownTimerNode;
    [Export] private PackedScene bombScene;
    [Export(PropertyHint.Range, "0,20,0.1")] private float speed = 10;

    public override void _Ready()
    {
        base._Ready();

        dashTimerNode.Timeout += HandleDashTimeout;

        CanTransition = () => cooldownTimerNode.IsStopped();
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_DASH);
        characterNode.Velocity = new(characterNode.direction.X, characterNode.direction.Y);

        if (characterNode.Velocity == Vector2.Zero)
        {
            characterNode.Velocity = characterNode.Sprite2DNode.FlipH ? Vector2.Left : Vector2.Right;
        }
        characterNode.Velocity *= speed;

        dashTimerNode.Start();

        Node2D bomb = bombScene.Instantiate<Node2D>();
        GetTree().CurrentScene.AddChild(bomb);
        bomb.GlobalPosition = characterNode.GlobalPosition;
    }

    private void HandleDashTimeout()
    {
        cooldownTimerNode.Start();
        characterNode.Velocity = Vector2.Zero;
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }
}
