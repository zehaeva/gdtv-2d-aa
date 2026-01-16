using Godot;
using System;

public partial class NPCBlackboard : Node
{
    // Movement Data
    public Vector2 WorkLocation { get; set; }
    public Vector2 HomeLocation { get; set; }
    public Node2D CurrentTarget { get; set; }

    // Status Data
    public float Hunger { get; set; } = 0;
    public bool IsInCombat { get; set; } = false;

    // Schedule Data
    public int CurrentWorkShift { get; set; }

}