using Godot;

public partial class InventoryItem : Node2D, IItem
{
    [Export] public int Amount {  get; set; }
    [Export] public Sprite2D Sprite { get; set; }
    [Export] public Label Label { get; set; }
    [Export] public BaseItem Item { get; set; }

    public string ItemName { get; set; }

    public string Description { get; set; }

    public Texture2D Icon { get; set; }

    public bool IsStackable { get; set; }

    #region Overrides
    public override void _Process(double delta)
    {
        this.Sprite.Texture = this.Icon;
        this.SetSpriteSizeTo(this.Sprite, new Vector2(42, 42));
        if (this.IsStackable)
        {
            this.Label.Text = this.Amount.ToString();
        }
        else
        {
            this.Label.Visible = false;
        }
    }
    #endregion

    public void SetData(string name, Texture2D icon, bool isStackable, int amount)
    {
        this.ItemName = name;
        this.Name = name;
        this.Icon = icon;
        this.IsStackable = isStackable;
        this.Amount = amount;
    }

    public void SetData(InventoryItem item)
    {
        this.ItemName = item.ItemName;
        this.Description = item.Description;
        this.Icon = item.Icon;
        this.IsStackable = item.IsStackable;
        this.Amount = item.Amount;
        this.Sprite = item.Sprite;
        this.Label = item.Label;
        this.Item = item.Item;
    }

    protected void SetSpriteSizeTo(Sprite2D sprite, Vector2 size)
    {
        Vector2 textureSize = sprite.Texture.GetSize();
        Vector2 scaleFactor = new Vector2(size.X / textureSize.X, size.Y / textureSize.Y);
        sprite.Scale = scaleFactor;
    }

    public void Fade() 
    {
        this.Sprite.Modulate = new Color(1, 1, 1, 0.4f);
        this.Label.Modulate = new Color(1, 1, 1, 0.4f);
    }
}
