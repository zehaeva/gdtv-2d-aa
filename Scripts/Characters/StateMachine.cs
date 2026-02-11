using Godot;
using System;
using System.Linq;
using System.Numerics;

public partial class StateMachine : Node
{
	[Export] protected Node currentState;
	[Export] protected CharacterState[] states;
	[Export] protected bool DebugState = false;

	public override void _Ready()
	{
		currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
	}

	public void SwitchState<T>()
	{
		CharacterState newState = states.Where((state) => state is T).FirstOrDefault();

		if (newState == null) { return; }
		if (currentState is T) { return; }

		this.SwitchState(newState);
	}

	// Type based version of Switch State
	public void SwitchState(Type stateType)
	{
		// instantiate the state object
		CharacterState newState = (CharacterState)Activator.CreateInstance(stateType);

		if (newState == null) { return; }

		if (currentState.GetType() == stateType) { return; }

		this.SwitchState(newState);
	}

	// generic private version of SwitchState
	private void SwitchState(CharacterState newState)
	{
		if (newState == null) { return; }

		if (!newState.CanTransition()) { return; }

		if (DebugState)
		{
			Node node = GetOwner();
			GD.Print(node.Name + ": Leaving state: " + currentState.Name + " Entering state: " + newState.Name);
		}

		currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
		currentState = newState;
		currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
	}
}
