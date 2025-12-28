using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class NPCSchedule : Node
{
    [Export] public Dictionary<int, NPCState> Schedule { get; set; }
}
