using Godot;
using System;
using System.Linq;

public partial class TreasureChest : StaticBody2D
{
    private bool can_interact = false;
    private bool isOpen = false;

    [Export] string ChestName;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (SceneManager.OpenedChests.Contains(ChestName))
        {
            isOpen = true;
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play(GameConstants.ANIM_OPEN);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_INTERACT) && can_interact)
        {
            if (!isOpen)
            {
                open_chest();
            }
        }
    }

    protected void open_chest()
    {
        if (!isOpen)
        {
            isOpen = true;
            GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play(GameConstants.ANIM_OPEN);
            GetNode<Sprite2D>("ScrollEmpty").Visible = true;
            GetNode<Timer>("Timer").Start();
            SceneManager.OpenedChests.Append(ChestName);
        }
    }

    protected void _on_timer_timeout()
    {
        GetNode<Sprite2D>("ScrollEmpty").Visible = false;
    }

}
