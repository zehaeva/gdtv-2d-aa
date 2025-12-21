using Godot;
using System.Linq;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private CharacterState[] states;
    [Export] private bool DebugState = false;

    public override void _Ready()
    {
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>()
    {
        CharacterState newState = states.Where((state) => state is T).FirstOrDefault();

        if (newState == null) { return; }

        if (currentState is T) { return; }

        if (!newState.CanTransition()) { return; }

        if (DebugState)
        {
            Node node = GetOwner();
            GD.Print(node.Name + ": Leaving state: " +  currentState.Name + " Entering state: " +  newState.Name);
        }

        currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        currentState = newState;
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }
}
