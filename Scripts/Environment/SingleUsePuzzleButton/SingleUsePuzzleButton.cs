using Godot;
using System;

public partial class SingleUsePuzzleButton : PuzzleButton
{
    protected override void _on_body_exited(Node2D body)
    { 
        // we don't unpress a single use button
    }
}
