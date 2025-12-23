using Godot;
using System;
using System.Linq;

public partial class TreasureChest : EnvironmentInteractable
{
    private bool isOpen = false;

    [Export] string ChestName;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        if (SceneManager.OpenedChests.Contains(ChestName))
        {
            GD.Print(ChestName + " chest was opened earlier");
            isOpen = true;
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play(GameConstants.ANIM_OPEN);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_INTERACT) && canInteract)
        {
            if (!isOpen)
            {
                open_chest();
            }
            else { GD.Print(ChestName + " chest was already opened"); }
        }
        else if(Input.IsActionJustPressed(GameConstants.INPUT_INTERACT))
         { GD.Print(ChestName + " chest can't interact " + canInteract); }
    }

    protected void open_chest()
    {
        if (!isOpen)
        {
            GD.Print(ChestName + " chest IS OPENING");
            isOpen = true;
            GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play(GameConstants.ANIM_OPEN);
            GetNode<Sprite2D>("ScrollEmpty").Visible = true;
            GetNode<Timer>("Timer").Start();
            SceneManager.OpenedChests.Append(ChestName);
        }
        else { GD.Print(ChestName + " chest was already opened"); }
    }

    protected void _on_timer_timeout()
    {
        GetNode<Sprite2D>("ScrollEmpty").Visible = false;
    }

}
