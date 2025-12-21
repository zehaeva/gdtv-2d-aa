using Godot;
using System;

public abstract partial class NPCState : CharacterState
{
    protected Vector2 destination;

    public override void _Ready()
    {
        base._Ready();

        characterNode.GetStatResource(Stat.HP).OnZero += HandleZeroHealth;
    }

    private void HandleZeroHealth()
    {
        characterNode.StateMachineNode.SwitchState<NPCDeathState>();
    }

    protected Vector2 GetPointGlobalPosition(int index)
    {
        Vector2 localPosition = Vector2.Zero;
        Vector2 globalPosition = characterNode.GlobalPosition;

        if (characterNode.PathNode != null)
        {
            if (index >= characterNode.PathNode.Curve.PointCount)
            {
                index = 0;
            };

            localPosition = characterNode.PathNode.Curve.GetPointPosition(index);
            globalPosition = characterNode.PathNode.GlobalPosition;
        }

        return (localPosition + globalPosition);
    }

    protected void Move()
    {
        if (characterNode.AgentNode != null)
        {
            characterNode.AgentNode.GetNextPathPosition();
        }

        //characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(destination) * characterNode.GetStatResource(Stat.Speed).StatValue;
        characterNode.Velocity = characterNode.Velocity.MoveToward(
                                    characterNode.GlobalPosition.DirectionTo(destination) * characterNode.GetStatResource(Stat.Speed).StatValue,
                                    characterNode.GetStatResource(Stat.Acceleration).StatValue
                                    );

        // update sprite facing direction
        Vector2 direction_normal = characterNode.Velocity.Normalized();

        switch (DirectionFacing)
        {
            case "":
                characterNode.AnimationPlayerNode.Stop();
                break;
            default:
                characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE + DirectionFacing);
                break;
        }

        characterNode.MoveAndSlide();
        //characterNode.Flip(); I don't think this is needed anymore
    }
}
