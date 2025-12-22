using Godot;
using System;

public partial class PlayerInteractState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.direction != Vector2.Zero)
        {
            characterNode.StateMachineNode.SwitchState<PlayerMoveState>();
        }
    }

    protected override void EnterState()
    {
        base.EnterState();
    }

    protected override void ExitState()
    {
        base.ExitState();
    }
}
