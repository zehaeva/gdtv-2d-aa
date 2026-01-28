using System;
using System.Collections.Generic;

public abstract partial class HeirarchicalState : NPCState
{
    protected NPCState _currentSubState;
    protected Dictionary<Type, NPCState> _subStates = new();

    public abstract void InitSubStates();

    protected override void EnterState()
    {
        InitSubStates();
    }

    public override void _Process(double delta)
    {
        _currentSubState?._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        _currentSubState?._PhysicsProcess(delta);
    }

    public void SwitchSubState<T>() where T : NPCState
    {
        Type type = typeof(T);
        if (!_subStates.ContainsKey(type)) { return; }

        _currentSubState?.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        _currentSubState = _subStates[type];
        _currentSubState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }
}
