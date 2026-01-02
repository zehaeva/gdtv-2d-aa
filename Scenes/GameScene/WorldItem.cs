using Godot;

public partial class WorldItem : Node2D
{
    private bool canInteract = false;

    private Area2D _area;
    [Export] BaseItem Item { get; set; }

    public override void _Ready()
    {
        base._Ready();

        //set the sprite based on the Item
        Sprite2D sprite = GetNode<Sprite2D>("Sprite2D");
        sprite.Texture = Item.Icon;

        // set up listening and pick up stuff
        _area = GetNode<Area2D>("Area2D");

        _area.AreaEntered += HandleAreaEntered;
        _area.AreaExited += HandleAreaExited;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_INTERACT) && canInteract)
        {
            // Send a signal that we're to be picked up
            GameEvents.RaiseItemPickup(Item);
            QueueFree();
        }
    }

    public override void _ExitTree()
    {
        _area.AreaEntered -= HandleAreaEntered;
        _area.AreaExited -= HandleAreaExited;
    }

    private void HandleAreaEntered(Area2D area)
    {
        if (area.OverlapsArea(_area))
        {
            canInteract = true;
        }
    }

    private void HandleAreaExited(Area2D area)
    {
        canInteract = false;
    }
}
