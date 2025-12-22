using Godot;
using System;
using System.Linq;

public partial class SlimeAttackState : SlimeState
{
    private Vector2 targetPosition;

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK + DirectionFacing);

        Node2D target = characterNode.AttackAreaNode.GetOverlappingBodies().First();
        destination = target.GlobalPosition;
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
        AnimationPlayerNode_AnimationFinished();
    }

    private void AnimationPlayerNode_AnimationFinished()
    {
        characterNode.ToggleHitBox(true);

        Node2D target = characterNode.AttackAreaNode.GetOverlappingBodies().FirstOrDefault();

        if (target == null)
        {
            Node2D chaseTarget = characterNode.ChaseAreaNode.GetOverlappingBodies().FirstOrDefault();
            if (chaseTarget == null)
            {
                characterNode.StateMachineNode.SwitchState<SlimeReturnState>();
            }
            characterNode.StateMachineNode.SwitchState<SlimeChaseState>();
            return;
        }

        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK + DirectionFacing);
        destination = target.GlobalPosition;
        
    }

    // Called from the AnimationPlayerNode
    protected void PerformHit()
    {
        characterNode.ToggleHitBox(false);
        characterNode.HitBoxNode.GlobalPosition = targetPosition;
    }
}
