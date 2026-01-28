using Godot;
using System;

public partial class Villager : NPC
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        NameLabel.Text = npc_name;

        //if we have a schedule we need to listen to the clock
        //if (Schedule != null)
        //{
        //    GameEvents.OnNextHour += GameEvents_OnNextHour;
        //}
    }

    private void GameEvents_OnNextHour(int obj)
    {
        GD.Print(String.Format("Next Hour: {0}", obj));
    }
}
