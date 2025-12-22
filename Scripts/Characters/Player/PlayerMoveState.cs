using Godot;
using System;
using static Godot.TextServer;

public partial class PlayerMoveState : PlayerState
{
    [Export(PropertyHint.Range, "0,20,0.1")] public float speed = 5;
    private bool can_interact = false;

    public override void _Ready()
    {
        characterNode.InteractArea2D.BodyEntered += InteractArea2D_BodyEntered;
        characterNode.InteractArea2D.BodyExited += InteractArea2D_BodyExited;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.direction == Vector2.Zero)
        {
            characterNode.Velocity = Vector2.Zero;
            characterNode.MoveAndSlide();

            characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
            return;
        }

        characterNode.Velocity = characterNode.Velocity.MoveToward(
                                    new Vector2(characterNode.direction.X, characterNode.direction.Y) * characterNode.GetStatResource(Stat.Speed).StatValue,
                                    characterNode.GetStatResource(Stat.Acceleration).StatValue);

        if (characterNode.Velocity.Y > 0)
        {
            characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE_DOWN);
            characterNode.InteractArea2D.Position = new Vector2(0, 8);
        }
        else if (characterNode.Velocity.Y < 0)
        {
            characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE_UP);
            characterNode.InteractArea2D.Position = new Vector2(0, -4);
        }
        else if (characterNode.Velocity.X > 0)
        {
            characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE_RIGHT);
            characterNode.InteractArea2D.Position = new Vector2(5, 2);
        }
        else if (characterNode.Velocity.X < 0)
        {
            characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE_LEFT);
            characterNode.InteractArea2D.Position = new Vector2(-5, 2);
        }
        else
        {
            characterNode.AnimationPlayerNode.Stop();
        }

        characterNode.MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        CheckForAttackInput();

        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
        else if(Input.IsActionJustPressed(GameConstants.INPUT_INTERACT) && !can_interact)
        {
            characterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
        else if (Input.IsActionJustPressed(GameConstants.INPUT_INTERACT) && can_interact)
        {
            characterNode.StateMachineNode.SwitchState<PlayerInteractState>();
        }
    }

    protected override void EnterState()
    {
        if (!String.IsNullOrEmpty(DirectionFacing))
        { characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE + DirectionFacing); }
    }

    private void InteractArea2D_BodyExited(Node2D body)
    {
        can_interact = false;
    }

    private void InteractArea2D_BodyEntered(Node2D body)
    {
        if (body.IsInGroup("interactable"))
        {
            can_interact = true;
        }
        else
        {
            can_interact = false;
        }
    }
}
