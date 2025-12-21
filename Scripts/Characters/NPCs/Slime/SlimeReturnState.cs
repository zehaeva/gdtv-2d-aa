using Godot;
using System;

public partial class SlimeReturnState : SlimeState
{
    public override void _Ready()
    {
        base._Ready();

        destination = GetPointGlobalPosition(0);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.AgentNode.IsNavigationFinished())
        {
            characterNode.StateMachineNode.SwitchState<SlimeWanderState>();
            return;
        }

        Move();
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE + DirectionFacing);

        characterNode.AgentNode.TargetPosition = destination;

        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }
}
