using Godot;
using System;

public partial class ExperiancePointsCostStrategy : CostStrategy
{
    public override bool CanPay(Node2D node)
    {
        return false;
    }

    public override void Pay(Node2D node)
    {
        throw new NotImplementedException();
    }
}
