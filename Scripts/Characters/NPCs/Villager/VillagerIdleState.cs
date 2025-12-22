using Godot;
using System;

public partial class VillagerIdleState : NPCState
{
    public override void _PhysicsProcess(double delta)
    {
        //GD.Print("IDLE Physics");
        if (characterNode.direction != Vector2.Zero)
        {
            characterNode.StateMachineNode.SwitchState<NPCMoveState>();
        }
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_IDLE);
        characterNode.InteractArea2D.BodyEntered += InteractArea2D_BodyEntered;
    }
    protected override void ExitState()
    {
        characterNode.InteractArea2D.BodyEntered -= InteractArea2D_BodyEntered;
    }

    private void InteractArea2D_BodyEntered(Node2D body)
    {
        characterNode.StateMachineNode.SwitchState<VillagerInteractState>();
    }
}
