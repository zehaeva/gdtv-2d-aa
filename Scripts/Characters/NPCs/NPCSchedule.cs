using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class NPCSchedule : Node
{
    [Export] public Dictionary<int, CharacterStates> Schedule { get; set; }

    [Export] private StateMachine _stateMachine;

    [Export] private bool Debug;

    // Mapping the Enum to the actual C# Class Types
    private static readonly System.Collections.Generic.Dictionary<CharacterStates, Type> StateMap = new()
    {
        { CharacterStates.IDLE, typeof(NPCMoveState) },
        { CharacterStates.INTERACT, typeof(NPCInteractState) },
        { CharacterStates.ATTACK, typeof(NPCAttackState) },
        { CharacterStates.DEATH, typeof(NPCDeathState) },
        { CharacterStates.MOVE, typeof(NPCMoveState) },
        { CharacterStates.CHASE, typeof(NPCMoveState) },
        { CharacterStates.WORK, typeof(NPCMoveState) },
        { CharacterStates.WANDER, typeof(NPCMoveState) },
        { CharacterStates.EAT, typeof(NPCMoveState) },
        { CharacterStates.SLEEP, typeof(NPCMoveState) },
        { CharacterStates.TRADE, typeof(NPCMoveState) }

    };

    public void CheckSchedule(int currentHour)
    {
        if (Schedule.ContainsKey(currentHour))
        {
            CharacterStates state = Schedule[currentHour];
            Type typeToSwitchTo = StateMap[state];

            _stateMachine.SwitchState(typeToSwitchTo);
            if (Debug)
            {
                GD.Print(String.Format("Changing state to {} at Hour {}", state, currentHour));
            }
        }
    }
}
