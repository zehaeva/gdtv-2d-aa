using Godot;

public partial class LessThanOrEqualsConstraint : ResourceConstraint
{
    [Export] public int MaxValue { get; set; }

    public bool IsMet(int value)
    {
        bool _return = false;

        if (MaxValue >= value)
        {
            _return = true;
        }

        return _return;
    }
}
