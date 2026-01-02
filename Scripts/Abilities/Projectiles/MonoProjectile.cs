
using Godot;

public partial class MonoProjectile : SpellBehaviour
{
    [Export] public PackedScene ProjectileScene { get; set; }  // the image/animation scene of the projectile
    [Export] public ProjectilePattern ProjectilePattern { get; set; } // how the projectile moves
    [Export] public float LifeTime { get; set; } = 2.0f;
    [Export] public int Speed { get; set; }

    public override void Cast(Node2D caster)
    {
        Projectile p = ProjectileScene.Instantiate() as Projectile;
        p.Pattern = ProjectilePattern.Duplicate();
        p.Velocity = new Vector2(1, 0) * 30;
        p.Rotation = 45.0f;
        p.Position = caster.ToGlobal(caster.Position);
        caster.GetTree().GetRoot().AddChild(p);
    }
}
