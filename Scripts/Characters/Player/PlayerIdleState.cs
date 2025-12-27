using Godot;

public partial class PlayerIdleState : PlayerState
{
    private bool canInteract = false;

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.direction != Vector2.Zero)
        {
            characterNode.StateMachineNode.SwitchState<PlayerMoveState>();
        }
    }

    public override void _Input(InputEvent @event)
    {
        CheckForAttackInput();

        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
        else if (Input.IsActionJustPressed(GameConstants.INPUT_INTERACT) && !canInteract)
        {
            characterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
        else if (Input.IsActionJustPressed(GameConstants.INPUT_INTERACT) && canInteract)
        {
            //characterNode.StateMachineNode.SwitchState<PlayerInteractState>();
        }
        else if (Input.IsActionJustPressed(GameConstants.INPUT_INVENTORY))
        {
            characterNode.StateMachineNode.SwitchState<PlayerInventoryState>();
        }
    }

    protected override void EnterState()
    {
        canInteract = false;

        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_IDLE + DirectionFacing);

        characterNode.InteractArea2D.BodyEntered += InteractArea2D_BodyEntered;
        characterNode.InteractArea2D.BodyExited += InteractArea2D_BodyExited;

        if (characterNode.InteractArea2D.HasOverlappingBodies())
        {
            foreach(Node2D node in characterNode.InteractArea2D.GetOverlappingBodies())
            {
                if (node.IsInGroup(GameConstants.GROUP_INTERACTABLE))
                {
                    canInteract = true;
                }
            }
        }
    }

    protected override void ExitState()
    {
        characterNode.InteractArea2D.BodyEntered -= InteractArea2D_BodyEntered;
        characterNode.InteractArea2D.BodyExited -= InteractArea2D_BodyExited;
    }

    private void InteractArea2D_BodyExited(Node2D body)
    {
        canInteract = false;
    }

    private void InteractArea2D_BodyEntered(Node2D body)
    {
        if (body.IsInGroup(GameConstants.GROUP_INTERACTABLE))
        {
            canInteract = true;
        }
        else
        {
            canInteract = false;
        }
    }
}
