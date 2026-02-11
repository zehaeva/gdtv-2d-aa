using Godot;
using System;

public abstract partial class CharacterState : Node
{
	protected Character characterNode;
	public Func<bool> CanTransition = () => true;

	public override void _Ready()
	{
		characterNode = GetOwner<Character>();
		SetPhysicsProcess(false);
		SetProcessInput(false);
	}

	public override void _Notification(int what)
	{
		base._Notification(what);

		if (what == GameConstants.NOTIFICATION_ENTER_STATE)
		{
			EnterState();
			SetPhysicsProcess(true);
			SetProcessInput(true);
		}
		else if (what == GameConstants.NOTIFICATION_EXIT_STATE)
		{
			ExitState();
			SetPhysicsProcess(false);
			SetProcessInput(false);
		}
	}

	protected virtual void EnterState() { }

	protected virtual void ExitState() { }

	protected string DirectionFacing
	{
		get
		{
			Vector2 direction_normal = Vector2.Zero;

			// update sprite facing direction
			if (characterNode.Velocity != Vector2.Zero)
			{
				direction_normal = characterNode.Velocity.Normalized();
			}
			else if (characterNode.lastDirection != Vector2.Zero)
			{
				direction_normal = characterNode.lastDirection.Normalized();
			}

			if (direction_normal.Y > 0.707)
			{
				return GameConstants.DOWN;
			}
			else if (direction_normal.Y < -0.707)
			{
				return GameConstants.UP;
			}
			else if (direction_normal.X > 0.707)
			{
				return GameConstants.RIGHT;
			}
			else if (direction_normal.X < -0.707)
			{
				return GameConstants.LEFT;
			}
			else { return ""; }
		}
	}
}
