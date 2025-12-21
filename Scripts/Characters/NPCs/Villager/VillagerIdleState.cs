using Godot;
using System;

public partial class VillagerIdleState : CharacterState
{
    public override void _PhysicsProcess(double delta)
    {
        //GD.Print("IDLE Physics");
        if (characterNode.direction != Vector2.Zero)
        {
            characterNode.StateMachineNode.SwitchState<PlayerMoveState>();
        }
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_IDLE);
    }
}
