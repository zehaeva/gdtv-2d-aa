using Godot;
using System;
using System.Linq;

public partial class VillagerInteractState : NPCState
{
    private int dialogue_index = 0;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (Input.IsActionJustPressed(GameConstants.INPUT_INTERACT))
        {
            // check that the player is facing us
            Area2D node = characterNode.InteractArea2D.GetOverlappingAreas().FirstOrDefault();
            if (node.GetParent() is Player)
            {
                //AudioStreamPlayer2DNode.Play();
                if (dialogue_index < characterNode.dialogue_lines.Length)
                {
                    characterNode.DialogueLayer.Visible = true;
                    characterNode.DialogueLabel.Text = characterNode.dialogue_lines[dialogue_index];
                    dialogue_index += 1;
                }
                else
                {
                    dialogue_index = 0;
                    characterNode.DialogueLayer.Visible = false;
                }
            }
        }
    }

    protected override void EnterState()
    {
        characterNode.InteractArea2D.AreaExited += InteractArea2D_BodyExited;
    }
    protected override void ExitState()
    {
        characterNode.DialogueLayer.Visible = false;
        dialogue_index = 0;
        characterNode.InteractArea2D.AreaExited -= InteractArea2D_BodyExited;
    }

    private void InteractArea2D_BodyExited(Node2D body)
    {
        characterNode.StateMachineNode.SwitchState<VillagerIdleState>();
    }
}
