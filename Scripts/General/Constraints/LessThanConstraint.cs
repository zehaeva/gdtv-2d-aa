using Godot;

public partial class LessThanConstraint : ResourceConstraint
{
    [Export] public int MaxValue { get; set; }

    public bool IsMet(int value)
    {
        bool _return = false;

        if (MaxValue > value)
        {
            _return = true;
        }

        return _return;
    }
}
