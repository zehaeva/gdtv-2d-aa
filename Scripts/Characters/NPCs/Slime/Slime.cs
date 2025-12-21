using Godot;

public partial class Slime : NPC
{
    Node2D target;
    [Export] private int speed = 30;
    [Export] private float acceleration = 15;
    [Export] public GpuParticles2D GPUParticles2DNode { get; set; }

    public override void _Ready()
    {
        base._Ready();

        //ChaseAreaNode.BodyEntered += _on_player_detect_area_2d_body_entered;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        //if (hp <= 0) { return; } // I'm dead! don't do anything!

        //chaseTarget();

        //animate_enemy();

        //MoveAndSlide();
    }

    private void chaseTarget()
    {
        //Vector2 distance_to_target = Vector2.Zero;
        //if (target is not null)
        //{
        //    distance_to_target = target.GlobalPosition - GlobalPosition;
        //}

        //Vector2 direction_normal = distance_to_target.Normalized();

        //Velocity = Velocity.MoveToward(direction_normal * speed, acceleration);
    }

    private void animate_enemy()
    {
        //Vector2 direction_normal = Velocity.Normalized();

        //if (direction_normal.Y > 0.707)
        //{
        //    AnimatedSprite2DNode.Play(GameConstants.ANIM_MOVE_DOWN);
        //}
        //else if (direction_normal.Y < -0.707)
        //{
        //    AnimatedSprite2DNode.Play(GameConstants.ANIM_MOVE_UP);
        //}
        //else if (direction_normal.X > 0.707)
        //{
        //    AnimatedSprite2DNode.Play(GameConstants.ANIM_MOVE_RIGHT);
        //}
        //else if (direction_normal.X < -0.707)
        //{
        //    AnimatedSprite2DNode.Play(GameConstants.ANIM_MOVE_LEFT);
        //}
        //else
        //{
        //    AnimatedSprite2DNode.Stop();
        //}
    }

    private void _on_player_detect_area_2d_body_entered(Node2D body)
    {
        if (body is Player)
        { target = body; }
    }

    private async void take_hit(int damage, Vector2 knockback)
    {
        DamageSFX.Play();

        Color flash_white_color = new Color(10, 0.5f, 0.5f);

        Modulate = flash_white_color;

        await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);

        Color original_color = new Color(1, 1, 1);

        Modulate = original_color;

        Velocity += knockback;

        hp -= damage;

        if (hp <= 0) { die(); }
    }

    private async void die()
    {
        GPUParticles2DNode.Emitting = true;
        //AnimationPlayerNode.Visible = false;
        CollisionShape2DNode.SetDeferred("disabled", true);

        await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);

        QueueFree();
    }
}
