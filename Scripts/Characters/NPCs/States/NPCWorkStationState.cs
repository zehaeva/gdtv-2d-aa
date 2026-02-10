using Godot;
using System;

public partial class NPCWorkStationState : NPCState
{
    public new StateType StateType = StateType.WORK;
    [Export] public Area2D WorkArea;

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_WORKING);

        // we need a set of hooks/signals to handle interupting states
        // attacked?

        // talked to?

        // suddenly died?
    }                                                                                                                                                                                                                                                                                                                                                                                                              
}
