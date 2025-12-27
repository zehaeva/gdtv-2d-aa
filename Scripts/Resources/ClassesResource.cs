using System;
using Godot;

[GlobalClass]
public partial class ClassesResource : GameResource
{
    public event Action OnXPGain;
    public event Action OnXPLoss;
    public event Action OnLevelGain;
    public event Action OnLevelLoss;

    public new string ResourceType { get => GameConstants.RESOURCE_CLASS; }

    [Export] public Classes ClassType { get; private set; }

    [Export] public PackedScene Class { get; private set; }

    private int _ClassLevel;
    private int _ExperiancePoints;

    [Export]
    public int ClassLevel
    {
        get => _ClassLevel;
        set
        {
            if (value < 0)
            {
                OnLevelLoss?.Invoke();
            }
            else
            {
                OnLevelGain?.Invoke();
            }

            _ClassLevel = value;
        }
    }

    [Export]
    public int ExperiancePoints
    {
        get => _ExperiancePoints;
        set
        {
            if (value < 0)
            {
                OnXPLoss?.Invoke();
            }
            else
            {
                OnXPGain?.Invoke();
            }

            _ExperiancePoints = value;
        }
    }


}
