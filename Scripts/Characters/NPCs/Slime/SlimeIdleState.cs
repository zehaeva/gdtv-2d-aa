using Godot;
using System;

public partial class SlimeIdleState : SlimeState
{
    public override void _PhysicsProcess(double delta)
    {
        //characterNode.StateMachineNode.SwitchState<SlimeReturnState>();
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_IDLE);
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }
}
