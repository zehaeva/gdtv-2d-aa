using Godot;
using System;

public partial class SpellStrategy : Resource
{
    [Export] public SpellBehaviour SpellBehaviour { get; set; }
    [Export] public CostStrategy Cost { get; set; }

    public void Cast(Node2D node)
    {
        if (Cost != null)
        {
            if (!Cost.CanPay(node))
            {
                return;
            }
        }

        Cost.Pay(node);
        SpellBehaviour.Cast(node);
    }
}
