using Godot;


public partial class UiRoot : CanvasLayer
{
    private Player _player;
    private InventoryDialog _dialog;

    public override void _Ready()
    {
        _player = GetNode<Player>("%Player");
        _dialog = GetNode<InventoryDialog>("%InventoryDialog");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed(GameConstants.INPUT_INVENTORY) && !_dialog.IsOpen)
        {
            _dialog.Open(_player.Inventory);
        }
    }
}
