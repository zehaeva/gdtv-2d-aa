using Godot;
using System;


public partial class NPCWorkState : HeirarchicalState
{
    [Export] public Area2D WorkArea;

    public override void InitSubStates()
    {
        this._stateType = StateType.WORK;

        // if not in work area go to work area
        if (!WorkArea.OverlapsBody(characterNode))
        {
            SwitchSubState<NPCMoveToAreaState>();
        }
        // start work step 1 ...
        else
        {
            SwitchSubState<NPCWorkStationState>();
        }
        // start work step 2 ...
        throw new NotImplementedException();
    }
}
