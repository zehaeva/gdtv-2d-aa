using Godot;

public partial class InventoryItem : Resource, IItem
{
    [Export] public int Amount {  get; set; }
    //[Export] public Sprite2D Sprite { get; set; }
    //[Export] public Label Label { get; set; }
    [Export] public BaseItem Item { get; set; }

    public string ItemName { get { return Item.ItemName; } }

    public string Description { get { return Item.Description; } }

    public Texture2D Icon { get { return Item.Icon; } }

    public bool IsStackable { get { return Item.IsStackable; } }

    #region Overrides
    //public override void _Process(double delta)
    //{
    //    this.Sprite.Texture = this.Icon;
    //    this.SetSpriteSizeTo(this.Sprite, new Vector2(42, 42));
    //    if (this.IsStackable)
    //    {
    //        this.Label.Text = this.Amount.ToString();
    //    }
    //    else
    //    {
    //        this.Label.Visible = false;
    //    }
    //}
    #endregion

    public void SetData(BaseItem item, int amount)
    {
        GD.Print("Setting Data: " + item.Description);
        this.Item = item;
        this.Amount = amount;
        //this.Sprite.Texture = item.Icon;
    }

    public void SetData(InventoryItem item)
    {
        this.Amount = item.Amount; 
        //this.Sprite = item.Sprite;
        //this.Label = item.Label;
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
        //this.Sprite.Modulate = new Color(1, 1, 1, 0.4f);
        //this.Label.Modulate = new Color(1, 1, 1, 0.4f);
    }
}
