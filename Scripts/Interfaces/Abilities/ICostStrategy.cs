using Godot;

public interface ICostStrategy
{
    public bool CanPay(Node2D node);

    public void Pay(Node2D node);
}
