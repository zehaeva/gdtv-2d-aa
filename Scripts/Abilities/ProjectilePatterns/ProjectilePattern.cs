using Godot;

public abstract partial class ProjectilePattern : Resource
{
    public abstract void Move(Projectile projectile, float delta);
}
