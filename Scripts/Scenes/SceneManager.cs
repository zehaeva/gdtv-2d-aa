using Godot;

public partial class SceneManager : Node2D
{
    [Export] Vector2 player_spawn_position;
    [Export] public int player_hp;

    public string[] opened_chests;
}
