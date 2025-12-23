using Godot;
using System;

public partial class SwitchPuzzleManager : Marker2D
{
    public Action PuzzleSolved;
    public Action PuzzleFailed;
    public void RaisePuzzleSolved() => PuzzleSolved?.Invoke();
    public void RaisePuzzleFailed() => PuzzleFailed?.Invoke();

    [Export] public int TargetScore { get; private set; } = 0;
    [Export] public Switch[] SwitchNodes { get; private set; }

    protected int score = 0;

    public override void _Ready()
    {
        foreach (Switch s in SwitchNodes)
        {
            s.Activated += SwitchActivated;
            s.Deactivated += SwitchDeactivated;
        }
    }

    public override void _ExitTree()
    {
        foreach (Switch s in SwitchNodes)
        {
            s.Activated -= SwitchActivated;
            s.Deactivated -= SwitchDeactivated;
        }
    }

    protected void SwitchActivated(int switchvalue)
    {
        score += switchvalue;

        if (score >= TargetScore)
        {
            GD.Print("puzzle solved!");
            RaisePuzzleSolved();
        }
        else
        {
            GD.Print("puzzle failed!");
            RaisePuzzleFailed();
        }
    }

    protected void SwitchDeactivated(int switchvalue)
    {
        score -= switchvalue;

        if (score < TargetScore)
        {
            GD.Print("puzzle failed!");
            RaisePuzzleFailed();
        }
    }
}
