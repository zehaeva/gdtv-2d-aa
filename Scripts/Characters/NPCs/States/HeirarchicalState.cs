using Godot;
using System;
using System.Linq;

public abstract partial class HeirarchicalState : NPCState
{
    protected NPCState _currentSubState;
    protected NPCState _lastSubState;
    [Export] protected NPCState[] SubStates;
    protected StateType _stateType;

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
        NPCState newState = SubStates?.Where((state) => state is T).FirstOrDefault();

        if (newState == null) { return; }
        if (_currentSubState is T) { return; }

        _currentSubState?.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        // only save the last "tracked" state type
        if (_currentSubState.StateType == _stateType)
        {
            _lastSubState = _currentSubState;
        }
        _currentSubState = newState;
        _currentSubState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void ReturnToLastSubState()
    {
        _currentSubState?.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        _currentSubState = _lastSubState;
        _currentSubState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }
}
