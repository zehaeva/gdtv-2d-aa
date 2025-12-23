using Godot;

public partial class SecretWallLayer : TileMapLayer
{
    [Export] public AudioStreamPlayer2D AudioStreamPlayer2DNode { get; private set; }
    [Export] public SwitchPuzzleManager PuzzleManager { get; private set; }

    public override void _Ready()
    {
        PuzzleManager.PuzzleSolved += DisableSecretWall;
        PuzzleManager.PuzzleFailed += EnableSecretWall;
    }

    public override void _ExitTree()
    {
        PuzzleManager.PuzzleSolved -= DisableSecretWall;
        PuzzleManager.PuzzleFailed -= EnableSecretWall;
    }

    protected void DisableSecretWall()
    {
        Visible = false;
        CollisionEnabled = false;
        AudioStreamPlayer2DNode.Play();
    }

    protected void EnableSecretWall()
    {
        Visible = true;
        CollisionEnabled = true;
    }
}
