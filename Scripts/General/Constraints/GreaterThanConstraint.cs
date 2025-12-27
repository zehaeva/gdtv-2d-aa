using Godot;

[GlobalClass]
public partial class GreaterThanConstraint : ResourceConstraint
{
    [Export] public int MinValue { get; set; }

    public bool IsMet(int value)
    {
        bool _return = false;

        if (MinValue < value)
        {
            _return = true;
        }

        return _return;
    }
}
