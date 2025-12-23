using Godot;

public partial class CaveEntrance : Area2D
{
    [Export] private string NextScene;
    [Export] private Vector2 PlayerSpawnPosition;

    private void _on_body_entered(Node2D body)
    {
        if (body is Player)
        {
            GD.Print("player has entered");

            SceneManager.player_spawn_position = PlayerSpawnPosition;

            GetTree().ChangeSceneToFile(NextScene);
        }
    }

    private void _on_body_exited(Node2D body)
    {
        if (body is Player)
        {
            GD.Print("player has exited");
        }
    }

}
