using Godot;
using System;

public partial class PlayerDeathState : PlayerState, IDeathState
{
    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_DEATH);

        characterNode.AnimationPlayerNode.AnimationFinished += AnimationPlayerNode_AnimationFinished;
    }

    private void AnimationPlayerNode_AnimationFinished(StringName animName)
    {
        GameEvents.RaiseEndGame();

        characterNode.QueueFree();
    }
}
