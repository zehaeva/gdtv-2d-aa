using Godot;
using System;
using System.Linq;
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

    #region Overrides
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();

        update_treasure_label();

        update_hp_bar();

        //Position = SceneManager.player_spawn_position;

        Engine.MaxFps = 60;

        ToggleHitBox(true);

        // link listeners
        GameEvents.NPCDied += HandleNPCDied;
        GameEvents.NPCKilled += HandleNPCKilled;
        GameEvents.OnItemPickup += HandleItemPickup;

        foreach (ClassesResource cr in this.Classes)
        {
            cr.LoadClass();
            UpdateClassAbilities(cr);
        }
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

        //push_blocks();

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

    public override void _ExitTree()
    {
        // Unlink listeners
        GameEvents.NPCDied -= HandleNPCDied;
        GameEvents.NPCKilled -= HandleNPCKilled;
    }
    #endregion

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

    private void update_treasure_label()
    {
        TreasureLabel.Text = GetStatResource(Stat.OpenedChests).StatValue.ToString();//SceneManager.opened_chests.Length.ToString();
    }

    #region Events
    private void HandleNPCDied(NPC npc)
    {

    }

    private void HandleItemPickup(BaseItem item)
    {
        GD.Print("I Picked up an item!" + item.Description);
        InventoryItem ii = new InventoryItem();
        ii.SetData(item, 1);
        Inventory.AddItem(ii);
    }

    private void HandleNPCKilled(Character killed, Character killer)
    {
        if (killer.Name == Name)
        {
            if (Classes != null)
            {            
                // don't count the classes that are maxxed out for purposes of xp splitting
                int classSplitCount = Classes.Where(x => x.ClassLevel < x.CharacterClass.MaxLevel).Count();
                int xp = (int)Mathf.Floor(killed.XPReward() / classSplitCount);

                foreach (ClassesResource item in Classes)
                {
                    item.ExperiancePoints += xp;

                    // if you now qualify for a level up go get it!

                    if(item.CharacterClass.CheckForLevelUp(this))
                    {
                        LevelUp(item);
                    }
                }
            }
        }
    }
    #endregion

    private void LevelUp(ClassesResource cr)
    {
        GD.Print("LEVEL UP!");
        cr.ClassLevel++;
        UpdateClassAbilities(cr);
    }

    private void UpdateClassAbilities(ClassesResource cr)
    {
        CharacterAbilities[] ca = cr.CharacterClass.GetCharacterAbilitiesByLevel(cr.ClassLevel);
        GD.Print(String.Format("Found {0} abilities!", ca.Length));
        foreach (CharacterAbilities cai in ca)
        {
            this.characterAbilities.Add(cai);
        }
    }

    private void update_hp_bar()
    {
        switch (GetStatResource(Stat.HP).StatValue)
        {
            case >= 3:
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

    private void reset_scene()
    {
        //SceneManager.player_hp = hp;

        GetTree().CallDeferred("reload_current_scene");
    }
}

