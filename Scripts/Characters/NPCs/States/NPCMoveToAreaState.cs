using Godot;
using System;

public partial class NPCMoveToAreaState : NPCState
{
    [Export] public Area2D DestinationArea;

    protected override void EnterState()
    {
        GD.Print("Move to area state?!?");
    }
}
