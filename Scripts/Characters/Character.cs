using Godot;
using System.Linq;

public abstract partial class Character : CharacterBody2D
{
    [Export] public StatResource[] Stats { get; set; }
    [Export] public ClassesResource[] Classes { get; set; }
    [Export] public AchievementResource[] Achievements { get; set; }

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
    [Export] public Area2D WanderAreaNode { get; private set; }
    [Export] public NavigationAgent2D AgentNode { get; private set; }
    [Export] public Area2D ChaseAreaNode { get; private set; }
    [Export] public Area2D AttackAreaNode { get; private set; }

    [ExportGroup("CharacterNodes")]
    [Export] public Inventory Inventory { get; private set; }

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
        if (area is not IHitBox hitbox) { return; }

        StatResource health = GetStatResource(Stat.HP);

        float damage = hitbox.GetDamage();

        health.StatValue -= damage;

        //shader.SetShaderParameter("active", true);
        HitFlashTimer.Start();

        // ------------------------------------

        DamageSFX.Play();

        GetStatResource(Stat.HP).StatValue -= damage;

        if (GetStatResource(Stat.HP).StatValue <= 0)
        {
            StateMachineNode.SwitchState<IDeathState>();
        }

        #region handle knockback
        Vector2 distance_to_enemy = GlobalPosition - area.GlobalPosition;

        Vector2 distance_normalized = distance_to_enemy.Normalized();

        float knockback_strength = 150;

        Velocity += distance_normalized * knockback_strength;

        Color flash_white_color = new Color(50, 50, 50);

        Modulate = flash_white_color;

        GetTree().CreateTimer(0.2);

        Color original_color = new Color(1, 1, 1);

        Modulate = original_color;
        #endregion
    }

    public StatResource GetStatResource(Stat stat)
    {
        StatResource result = null;
        if (Stats != null)
        {
            result = Stats.Where((element) => element.StatType == stat).FirstOrDefault();
        }

        return result;
    }

    public ClassesResource GetClassesResource(Classes stat)
    {
        ClassesResource result = null;
        if (Classes != null)
        {
            result = Classes.Where((element) => element.ClassType == stat).FirstOrDefault();
        }

        return result;
    }

    public AchievementResource GetAchievementResource(Achievement achievement)
    {
        AchievementResource result = null;
        if (Achievements != null)
        {
            result = Achievements.Where((element) => element.AchievementType == achievement).FirstOrDefault();
        }

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
