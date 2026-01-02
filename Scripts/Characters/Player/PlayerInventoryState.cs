using Godot;

public partial class PlayerInventoryState : PlayerState
{
    #region Overrides
    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_INVENTORY) ||
            Input.IsActionJustPressed(GameConstants.INPUT_ESC))
        {
            characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
        }
    }
    #endregion

        #region Enter and Exit State
    protected override void EnterState()
    {
        base.EnterState();
        //characterNode.Inventory.Visible = true;
    }

    protected override void ExitState() 
    { 
        base.ExitState();
        //characterNode.Inventory.Visible = false;
    }
    #endregion
}
