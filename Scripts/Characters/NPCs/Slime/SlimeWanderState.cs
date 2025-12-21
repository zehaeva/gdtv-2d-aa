using Godot;
using System;

public partial class SlimeWanderState : SlimeState
{
    [Export] private Timer idleTimerNode;
    [Export(PropertyHint.Range, "0, 20, 0.1")] private float maxIdleTime;

    private int pointIndex = 0;

    public override void _PhysicsProcess(double delta)
    {
        if (!idleTimerNode.IsStopped())
        { return; }

        Move();
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE + DirectionFacing);

        pointIndex = 1;

        destination = GetPointGlobalPosition(pointIndex);
        characterNode.AgentNode.TargetPosition = destination;

        characterNode.AgentNode.NavigationFinished += HandleNavigationFinished;
        idleTimerNode.Timeout += HandleTimeout;
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.AgentNode.NavigationFinished -= HandleNavigationFinished;
        idleTimerNode.Timeout -= HandleTimeout;
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    private void HandleTimeout()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_MOVE + DirectionFacing);

        pointIndex = Mathf.Wrap(pointIndex + 1, 0, characterNode.PathNode.Curve.PointCount);

        destination = GetPointGlobalPosition(pointIndex);
        characterNode.AgentNode.TargetPosition = destination;
    }

    private void HandleNavigationFinished()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_IDLE);

        RandomNumberGenerator rng = new RandomNumberGenerator();

        idleTimerNode.WaitTime = rng.RandfRange(0, maxIdleTime);

        idleTimerNode.Start();
    }
}
