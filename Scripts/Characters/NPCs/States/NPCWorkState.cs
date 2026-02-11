using Godot;
using System;


public partial class NPCWorkState : HeirarchicalState
{
    [Export] public Area2D WorkArea;
    // we need a list of action to perform, 
    // either a queue, or a list of actions with timers to perform them at the right time
    // each action could have prequisites, like "needs to be near a certain object", "needs to have a certain item in inventory", etc.

    public override void InitSubStates()
    {
        this._stateType = StateType.WORK;

        // if not in work area go to work area
        if (WorkArea != null && !WorkArea.OverlapsBody(characterNode))
        {
            SwitchSubState<NPCMoveToAreaState>();
        }
        // start work step 1 ...
        else
        {
            SwitchSubState<NPCWorkStationState>();
        }
        // start work step 2 ...
    }
}
