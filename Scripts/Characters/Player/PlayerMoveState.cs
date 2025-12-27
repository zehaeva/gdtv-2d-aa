using Godot;
using System;
using static Godot.TextServer;

public partial class PlayerMoveState : PlayerState
{
    [Export(PropertyHint.Range, "0,20,0.1")] public float speed = 5;
    private bool can_interact = false;

    #region Overrides
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

        #region Animation and Facing Direction
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
        #endregion

        #region Push Blocks
        // Get the last collision
        // Check if it's a block and if it's a block then push it
        KinematicCollision2D collision = characterNode.GetLastSlideCollision();

        if (collision is not null && collision.GetCollider().IsClass("RigidBody2D"))
        {
            RigidBody2D collider_node = (RigidBody2D)collision.GetCollider();
            Vector2 collision_normal = Vector2.Zero;

            if (collider_node.IsInGroup(GameConstants.GROUP_PUSHABLE))
            {
                collision_normal = collision.GetNormal();
            }

            collider_node.ApplyCentralForce(collision_normal * -1 * characterNode.GetStatResource(Stat.Strength).StatValue);
        }
        #endregion

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
        else if (Input.IsActionJustPressed(GameConstants.INPUT_INVENTORY))
        {
            characterNode.StateMachineNode.SwitchState<PlayerInventoryState>();
        }
    }

    protected override void EnterState()
    {
        if (!String.IsNullOrEmpty(DirectionFacing))
        { characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE + DirectionFacing); }

        characterNode.InteractArea2D.BodyEntered += InteractArea2D_BodyEntered;
        characterNode.InteractArea2D.BodyExited += InteractArea2D_BodyExited;
    }

    protected override void ExitState()
    {
        characterNode.InteractArea2D.BodyEntered -= InteractArea2D_BodyEntered;
        characterNode.InteractArea2D.BodyExited -= InteractArea2D_BodyExited;
    }
    #endregion

    #region Event Handlers
    private void InteractArea2D_BodyExited(Node2D body)
    {
        can_interact = false;
    }

    private void InteractArea2D_BodyEntered(Node2D body)
    {
        if (body.IsInGroup(GameConstants.GROUP_INTERACTABLE))
        {
            can_interact = true;
        }
        else
        {
            can_interact = false;
        }
    }
    #endregion
}
