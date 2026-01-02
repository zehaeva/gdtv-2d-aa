using Godot;

public abstract partial class CostStrategy : Resource, ICostStrategy
{
    public abstract bool CanPay(Node2D node);

    public abstract void Pay(Node2D node);
}
