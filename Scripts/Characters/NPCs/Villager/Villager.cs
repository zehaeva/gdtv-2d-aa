using Godot;
using System;

public partial class Villager : NPC
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        NameLabel.Text = npc_name;
    }
}
