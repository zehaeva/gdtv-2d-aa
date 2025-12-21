using Godot;
using System;

public abstract partial class SlimeState : NPCState
{
    protected void HandleChaseAreaBodyEntered(Node2D body)
    {
        characterNode.StateMachineNode.SwitchState<SlimeChaseState>();
    }
}
