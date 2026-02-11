using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class NPCScheduleManager : Node
{
    [Export] public Dictionary<int, ScheduleStates> Schedule { get; set; }

    [Export] private StateMachine _stateMachine;

    [Export] private bool Debug;

    // Mapping the Enum to the actual C# Class Types
    private static readonly System.Collections.Generic.Dictionary<ScheduleStates, Type> StateMap = new()
    {
        { ScheduleStates.WORK, typeof(NPCWorkState) },
        { ScheduleStates.HOME, typeof(NPCHomeState) },
        { ScheduleStates.SOCIALIZE, typeof(NPCSocializeState) },
        { ScheduleStates.SLEEP, typeof(NPCSleepState) }

    };

    public override void _Ready()
    {
        GameEvents.OnNextHour += CheckSchedule;
        CheckSchedule(GameClock.Instance.CurrentHour);
    }
    public override void _ExitTree()
    {
        GameEvents.OnNextHour -= CheckSchedule;
    }

    public void CheckSchedule(int currentHour)
    {
        if (Schedule.ContainsKey(currentHour))
        {
            ScheduleStates state = Schedule[currentHour];
            Type typeToSwitchTo = StateMap[state];

            _stateMachine.SwitchState(typeToSwitchTo);
            if (Debug)
            {
                GD.Print(String.Format("Changing state to {0} at Hour {1}", state, currentHour));
            }
        }
    }
}
