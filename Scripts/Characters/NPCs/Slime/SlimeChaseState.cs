using Godot;
using System;
using System.Linq;

public partial class SlimeChaseState : SlimeState
{
    [Export] private Timer ChaseTimerNode;

    private CharacterBody2D target;

    protected override void EnterState()
    {
        //characterNode.AnimatedSprite2DNode.Play(GameConstants.ANIM_MOVE + DirectionFacing);
        target = characterNode.ChaseAreaNode.GetOverlappingBodies().First() as CharacterBody2D;

        ChaseTimerNode.Timeout += HandleTimeout;

        characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
        characterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
    }

    protected override void ExitState()
    {
        ChaseTimerNode.Timeout -= HandleTimeout;
        characterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
        characterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    private void HandleTimeout()
    {
        destination = target.GlobalPosition;
        characterNode.AgentNode.TargetPosition = destination;
    }

    protected void HandleChaseAreaBodyExited(Node2D body)
    {
        characterNode.StateMachineNode.SwitchState<SlimeReturnState>();
    }

    protected void HandleAttackAreaBodyEntered(Node2D body)
    {
        characterNode.StateMachineNode.SwitchState<SlimeAttackState>();
    }
}
