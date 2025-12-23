using Godot;

public partial class LockedDoor : StaticBody2D
{
    [Export] public int ButtonsNeeded = 1;

    [Export] public CollisionShape2D CollisionShape2DNode { get; private set; }
    [Export] public PuzzleButton[] PuzzleButtonNode { get; private set; }

    private int buttonsPressed = 0;

    public override void _Ready()
    {
        foreach (PuzzleButton pb in PuzzleButtonNode)
        {
            GD.Print("linking!");
            pb.Pressed += Area2DNode_AreaEntered;
            pb.UnPressed += Area2DNode_AreaExited;
        }
    }

    public override void _ExitTree()
    {
        foreach (PuzzleButton pb in PuzzleButtonNode)
        {
            pb.Pressed -= Area2DNode_AreaEntered;
            pb.UnPressed -= Area2DNode_AreaExited;
        }
    }

    private void Area2DNode_AreaEntered()
    {
        buttonsPressed += 1;

        if (ButtonsNeeded == buttonsPressed)
        {
            Visible = false;
            CollisionShape2DNode.SetDeferred("disabled", true);
        }
    }

    private void Area2DNode_AreaExited()
    {
        buttonsPressed -= 1;
        if (ButtonsNeeded != buttonsPressed)
        {
            Visible = true;
            CollisionShape2DNode.SetDeferred("disabled", false);
        }
    }
}