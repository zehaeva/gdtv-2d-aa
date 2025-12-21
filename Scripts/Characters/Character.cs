using Godot;
using System.Linq;

public abstract partial class Character : CharacterBody2D
{
    [Export] private StatResource[] stats;

    [ExportGroup("RequiredNodes")]
    [Export] public AnimationPlayer AnimationPlayerNode { get; private set; }
    [Export] public Sprite2D Sprite2DNode { get; private set; }
    [Export] public StateMachine StateMachineNode { get; private set; }
    [Export] public Area2D HurtBoxNode { get; private set; }
    [Export] public Area2D HitBoxNode { get; private set; }
    [Export] public CollisionShape2D CollisionShape2DNode { get; private set; }
    [Export] public CollisionShape2D HitBoxShapeNode { get; private set; }
    [Export] public Timer HitFlashTimer { get; private set; }
    [Export] public AudioStreamPlayer2D DamageSFX { get; private set; }
    [Export] public Area2D InteractArea2D { get; private set; }


    [ExportGroup("AINodes")]
    [Export] public Path2D PathNode { get; private set; }
    [Export] public NavigationAgent2D AgentNode { get; private set; }
    [Export] public Area2D ChaseAreaNode { get; private set; }
    [Export] public Area2D AttackAreaNode { get; private set; }

    public Vector2 direction = new();
    public Vector2 lastDirection;

    private ShaderMaterial shader;

    public override void _Ready()
    {
        //shader = (ShaderMaterial)Sprite2DNode.MaterialOverlay;

        HurtBoxNode.AreaEntered += HandleHurtBoxEntered;
        //Sprite2DNode.TextureChanged += HandleTextureChanged;
        HitFlashTimer.Timeout += HandleHitFlashTimeout;
    }

    private void HandleHitFlashTimeout()
    {
        //shader.SetShaderParameter("active", false);
    }

    private void HandleTextureChanged()
    {
        //shader.SetShaderParameter("tex", Sprite2DNode.Texture);
    }

    private void HandleHurtBoxEntered(Area2D area)
    {
        //if (area is not IHitBox hitbox) { return; }

        StatResource health = GetStatResource(Stat.HP);

        //float damage = hitbox.GetDamage();

        //health.StatValue -= damage;

        //shader.SetShaderParameter("active", true);
        HitFlashTimer.Start();
    }

    public StatResource GetStatResource(Stat stat)
    {
        StatResource result = stats.Where((element) => element.StatType == stat).FirstOrDefault();

        return result;
    }

    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;

        if (isNotMovingHorizontally) { return; }

        bool isMovingLeft = Velocity.X < 0;
        Sprite2DNode.FlipH = isMovingLeft;
    }

    public void ToggleHitBox(bool flag)
    {
        HitBoxShapeNode.Disabled = flag;
    }
}
