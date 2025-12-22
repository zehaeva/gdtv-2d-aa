using Godot;
using System;
using static Godot.TextServer;

public partial class PlayerAttackState : PlayerState
{

    public override void _Ready()
    {
        base._Ready();
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK + DirectionFacing);
        characterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;

        characterNode.HitBoxNode.BodyEntered += HandleBodyEntered;
    }
    protected override void ExitState()
    {
        characterNode.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
        characterNode.HitBoxNode.BodyEntered -= HandleBodyEntered;
    }

    private void HandleBodyEntered(Node2D body)
    {
    }

    private void HandleAnimationFinished(StringName animName)
    {
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
        characterNode.ToggleHitBox(true);
    }

    // Called from the Animation Player
    private void PerformHit()
    {
        characterNode.ToggleHitBox(false);

        characterNode.Velocity = new Vector2(0, 0);
    }
}
