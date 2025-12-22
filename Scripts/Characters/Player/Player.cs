using Godot;
using System.Text.RegularExpressions;

public partial class Player : Character
{
    [Export] public Timer DeathTimer;
    [Export] public Timer AttackDurationTimer;
    [Export] public Sprite2D Sword;
    [Export] public Label TreasureLabel;
    [Export] public AnimatedSprite2D HPBar;

    [Export] private float move_speed = 100;
    [Export] private float push_strength = 300;
    [Export] private float acceleration = 15;


    public bool attack_hit { get; private set; } = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();

        update_treasure_label();

        update_hp_bar();

        //Position = SceneManager.player_spawn_position;

        Engine.MaxFps = 60;

        ToggleHitBox(true);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        //if (SceneManager.player_hp <= 0) { return; }

        //if (!is_attacking)
        //{
        //    move_player();
        //}

        push_blocks();

        update_treasure_label();

        //MoveAndSlide();

        //if (Input.IsActionJustPressed(GameConstants.INPUT_INTERACT) && can_attack)
        //{ attack(); }
    }

    public override void _Input(InputEvent @event)
    {
        this.direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT,
                                         GameConstants.INPUT_MOVE_RIGHT,
                                         GameConstants.INPUT_MOVE_UP,
                                         GameConstants.INPUT_MOVE_DOWN);
        if (this.direction != Vector2.Zero)
        {
            this.lastDirection = this.direction;
        }

    }

    private void move_player()
    {
        var move_vector = Input.GetVector(GameConstants.ANIM_MOVE_LEFT, 
                                          GameConstants.ANIM_MOVE_RIGHT, 
                                          GameConstants.ANIM_MOVE_UP, 
                                          GameConstants.ANIM_MOVE_DOWN);


        Velocity = Velocity.MoveToward(move_vector * move_speed, acceleration);

        if (Velocity.Y > 0)
        {
            base.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE_DOWN);
            InteractArea2D.Position = new Vector2(0, 8);
        }
        else if (Velocity.Y < 0)
        {
            base.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE_UP);
            InteractArea2D.Position = new Vector2(0, -4);
        }
        else if (Velocity.X > 0)
        {
            base.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE_RIGHT);
            InteractArea2D.Position = new Vector2(5, 2);
        }
        else if (Velocity.X < 0)
        {
            base.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE_LEFT);
            InteractArea2D.Position = new Vector2(-5, 2);
        }
        else
        {
            base.AnimationPlayerNode.Stop();
        }
    }

    private void push_blocks()
    {
        // Get the last collision
        // Check if it's a block and if it's a block then push it
        KinematicCollision2D collision = GetLastSlideCollision();

        if (collision is not null && collision.GetCollider().IsClass("RigidBody2D"))
        {
            RigidBody2D collider_node = (RigidBody2D)collision.GetCollider();
            Vector2 collision_normal = Vector2.Zero;

            if (collider_node.IsInGroup("pushable"))
            {
                collision_normal = collision.GetNormal();
            }

            collider_node.ApplyCentralForce(collision_normal * -1 * push_strength);
        }
    }

    private void _on_area_2d_body_exited(Node2D body)
    {
        if (body.IsInGroup("interactable"))
        {
            //body.can_interact = false;
        }

        //can_attack = true;
    }

    private void _on_area_2d_body_entered(Node2D body)
    {
        if (body.IsInGroup("interactable"))
        {
            //body.can_interact = true;
        }

        //can_attack = false;
    }

    private void update_treasure_label()
    {
        TreasureLabel.Text = GetStatResource(Stat.OpenedChests).StatValue.ToString();//SceneManager.opened_chests.Length.ToString();
    }

    private void _on_hitbox_area_2d_body_entered(Node2D body)
    {
        DamageSFX.Play();

        //SceneManager.player_hp -= 1;
        GetStatResource(Stat.HP).StatValue -= 1;

        update_hp_bar();

        //if (SceneManager.player_hp <= 0)
        if (GetStatResource(Stat.HP).StatValue <= 0)
        {
            die();
        }

        Vector2 distance_to_enemy = GlobalPosition - body.GlobalPosition;

        Vector2 distance_normalized = distance_to_enemy.Normalized();

        float knockback_strength = 150;

        Velocity += distance_normalized * knockback_strength;

        Color flash_white_color = new Color(50, 50, 50);

        Modulate = flash_white_color;

        GetTree().CreateTimer(0.2);

        Color original_color = new Color(1, 1, 1);

        Modulate = original_color;
    }

    private void die()
    {
        if (!DeathTimer.IsStopped())
        { return; }

        base.AnimationPlayerNode.Play(GameConstants.ANIM_DEATH);

        DeathTimer.Start();
    }

    private void update_hp_bar()
    {
        switch (GetStatResource(Stat.HP).StatValue)
        {
            case 3:
                HPBar.Play("3_hp");
                break;
            case 2:
                HPBar.Play("2_hp");
                break;
            case 1:
                HPBar.Play("1_hp");
                break;
            default:
                HPBar.Play("0_hp");
                break;
        }
    }

    private void attack()
    {
        //if (!AttackDurationTimer.IsStopped()) { return; }

        ////is_attacking = true;
        //Sword.Visible = true;
        ////Sword / SwordArea2D.monitoring = true;
        ////Sword / SwordSFX.Play();
        //AttackDurationTimer.Start();

        //Velocity = new Vector2(0, 0);

        //switch (base.AnimationPlayerNode.Animation)
        //{
        //    case GameConstants.ANIM_MOVE_UP:
        //        base.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK_UP);
        //        AnimationPlayerNode.Play("attack_sword_up");
        //        break;
        //    case GameConstants.ANIM_MOVE_RIGHT:
        //        base.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK_RIGHT);
        //        AnimationPlayerNode.Play("attack_sword_right");
        //        break;
        //    case GameConstants.ANIM_MOVE_DOWN:
        //        base.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK_DOWN);
        //        AnimationPlayerNode.Play("attack_sword_down");
        //        break;
        //    case GameConstants.ANIM_MOVE_LEFT:
        //        base.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK_LEFT);
        //        AnimationPlayerNode.Play("attack_sword_left");
        //        break;
        //}
    }

    private void _on_sword_area_2d_body_entered(Node2D body)
    {
        if (attack_hit)
        { return; }

        attack_hit = true;

        Vector2 distance_to_enemy = body.GlobalPosition - GlobalPosition;

        Vector2 knockback_direction = distance_to_enemy.Normalized();

        float knockback_strength = 150;

        //body.take_hit(1, knockback_direction * knockback_strength);
    }

    private void _on_attack_duration_timer_timeout()
    {
        ////is_attacking = false;

        //attack_hit = false;
        //Sword.Visible = false;
        ////Sword / SwordArea2D.monitoring = false;

        //AnimationPlayerNode.Play("RESET");

        //switch (base.AnimationPlayerNode.Animation)
        //{
        //    case GameConstants.ANIM_ATTACK_UP:
        //        base.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE_UP);
        //        break;

        //    case GameConstants.ANIM_ATTACK_RIGHT:
        //        base.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE_RIGHT);
        //        break;

        //    case GameConstants.ANIM_ATTACK_DOWN:
        //        base.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE_DOWN);
        //        break;

        //    case GameConstants.ANIM_ATTACK_LEFT:
        //        base.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE_LEFT);
        //        break;
        //}
    }

    private void reset_scene()
    {
        //SceneManager.player_hp = hp;

        GetTree().CallDeferred("reload_current_scene");
    }
}

