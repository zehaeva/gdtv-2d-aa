using Godot;

public partial class ToolTip : ColorRect
{
    MarginContainer MarginContainer { get; set; }
    Label ItemName { get; set; }

    public override void _Ready()
    {
        base._Ready();
        MarginContainer = GetNode<MarginContainer>("MarginContainer");
        ItemName = GetNode<Label>("MarginContainer/ItemName");
    }

    public void SetText(string text)
    {
        this.ItemName.Text = text;
        MarginContainer.Size = new Vector2();
        Size = MarginContainer.Size;
    }
}
