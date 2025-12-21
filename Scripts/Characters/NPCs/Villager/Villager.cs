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

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_INTERACT) && can_interact)
        {   //AudioStreamPlayer2DNode.Play();
            if (dialogue_index < dialogue_lines.Length)
            {
                DialogueLayer.Visible = true;
                DialogueLabel.Text = dialogue_lines[dialogue_index];
                dialogue_index += 1;
                ////# get_tree().paused = $CanvasLayer.visible;}
            }
            else
            {
                dialogue_index = 0;
                DialogueLayer.Visible = false;
            }
        }

        if (!can_interact)
        {
            DialogueLayer.Visible = false;
            dialogue_index = 0;
        }
    }
}
