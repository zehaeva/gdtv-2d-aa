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
        //else if (Input.IsActionJustPressed(GameConstants.INPUT_INVENTORY))
        //{
        //    characterNode.StateMachineNode.SwitchState<PlayerInventoryState>();
        //}
    }

    protected override void EnterState()
    {
        canInteract = false;

        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_IDLE + DirectionFacing);

        // handle changes, elements entering exiting the area
        characterNode.InteractArea2D.BodyEntered += InteractArea2D_BodyEntered;
        characterNode.InteractArea2D.BodyExited += InteractArea2D_BodyExited;
        characterNode.InteractArea2D.AreaEntered += InteractArea2D_AreaEntered;
        characterNode.InteractArea2D.AreaExited += InteractArea2D_AreaExited;

        // hand things that are in the area when we swap to idle
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
        if (characterNode.InteractArea2D.HasOverlappingAreas())
        {
            foreach (Node2D node in characterNode.InteractArea2D.GetOverlappingAreas())
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
        characterNode.InteractArea2D.AreaEntered -= InteractArea2D_AreaEntered;
        characterNode.InteractArea2D.AreaExited -= InteractArea2D_AreaExited;
    }

    private void CanInteract(Node2D node)
    {
        if (node.IsInGroup(GameConstants.GROUP_INTERACTABLE))
        {
            canInteract = true;
        }
        else
        {
            canInteract = false;
        }
    }

    private void InteractArea2D_BodyExited(Node2D body)
    {
        canInteract = false;
    }

    private void InteractArea2D_BodyEntered(Node2D body)
    {
        CanInteract(body);
    }

    private void InteractArea2D_AreaExited(Node2D area)
    {
        canInteract = false;
    }

    private void InteractArea2D_AreaEntered(Node2D area)
    {
        CanInteract(area);
    }
}
