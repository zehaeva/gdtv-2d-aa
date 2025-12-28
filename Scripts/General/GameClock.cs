using Godot;
using System;

public partial class GameClock : Node
{
    public int CurrentHour { get; private set; }

    [Export] public int SecondsPerHour { get; private set; }
    [Export] public int HoursPerDay { get; private set; }
    [Export] public Timer GlobalClock { get; private set; }

    public override void _Ready()
    {
        GlobalClock.Timeout += NextHour;
        CurrentHour = 0;
        NextHour();
    }

    public void NextHour()
    {
        GlobalClock.WaitTime = SecondsPerHour;

        if (CurrentHour > HoursPerDay)
        {
            CurrentHour = 0;
        }
        else
        {
            CurrentHour++;
        }

        GameEvents.RaiseNextHour(CurrentHour);
        GlobalClock.Start();
    }
}

