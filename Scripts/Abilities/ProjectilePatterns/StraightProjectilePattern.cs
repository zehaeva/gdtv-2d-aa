using Godot;

public partial class StraightProjectilePattern : ProjectilePattern
{
    [Export] float Duration { get; set; } = 0.5f;
    [Export] float Speed { get; set; } = 70.0f;

    private Vector2 randomVector = new Vector2();
    private float timeElapsed = 0.0f;

    public override void Move(Projectile projectile, float delta)
    {
        if (timeElapsed <= 0.0f)
        {
            RandomNumberGenerator rand = new RandomNumberGenerator();
            randomVector = new Vector2(rand.RandfRange(-1, 1), rand.RandfRange(-1, 1));
            projectile.Velocity = randomVector * Speed;
            timeElapsed = Duration;
        }

        projectile.Position += projectile.Velocity * delta;
        timeElapsed -= delta;
    }
}
