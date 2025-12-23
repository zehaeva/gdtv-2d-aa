using Godot;

public static class SceneManager
{
    [Export] public static Vector2 player_spawn_position;
    [Export] public static int player_hp;

    public static string[] OpenedChests = [];
}
