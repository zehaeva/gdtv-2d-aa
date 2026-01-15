using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class NPCSchedule : Node
{
    [Export] public Dictionary<int, CharacterStates> Schedule { get; set; }

    [Export] private StateMachine _stateMachine;

    // Mapping the Enum to the actual C# Class Types
    private static readonly System.Collections.Generic.Dictionary<CharacterStates, Type> StateMap = new()
    {
        { CharacterStates.IDLE, typeof(NPCMoveState) },
        { CharacterStates.INTERACT, typeof(NPCInteractState) },
        { CharacterStates.ATTACK, typeof(NPCAttackState) },
        { CharacterStates.DEATH, typeof(NPCDeathState) },
        { CharacterStates.MOVE, typeof(NPCMoveState) }
    };
}
