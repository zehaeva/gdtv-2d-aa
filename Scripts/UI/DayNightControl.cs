using Godot;
using System;

// this just handles the graphical display of the day/night cycle
public partial class DayNightControl : PanelContainer
{
    [Export] public MarginContainer DialContainer { get; protected set; }

    public override void _Ready()
    {
        RotateToHour(1);
        GameEvents.OnNextHour += GameEvents_OnNextHour;
    }
    public override void _ExitTree()
    {
        GameEvents.OnNextHour -= GameEvents_OnNextHour;
    }

    private void GameEvents_OnNextHour(int obj)
    {
        RotateToHour(obj);
    }

    public void RotateToHour(int hour)
    {
        double degreesPerHour = 2 * Math.PI / GameClock.Instance.HoursPerDay;

        double moveTo = degreesPerHour + Rotation;

        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(this, "rotation", (float)moveTo, GameClock.Instance.SecondsPerHour);
    }
}  

            