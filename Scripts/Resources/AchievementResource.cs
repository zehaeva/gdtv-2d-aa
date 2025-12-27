using System;
using Godot;

[GlobalClass]
public partial class AchievementResource : GameResource
{
    public event Action OnGain;
    public event Action OnLoss;

    public new string ResourceType { get => GameConstants.RESOURCE_ACHIEVEMENT; }

    [Export] public Achievement AchievementType { get; private set; }

    private bool _AchievementValue;
    [Export]
    public bool AchivementValue
    {
        get => _AchievementValue;
        set
        {
            bool oldValue = _AchievementValue;

            _AchievementValue = value;

            if (oldValue != value)
            {
                if (value)
                {
                    OnGain?.Invoke();
                }
                else
                {
                    OnLoss?.Invoke();
                }
            }
        }
    }


}
