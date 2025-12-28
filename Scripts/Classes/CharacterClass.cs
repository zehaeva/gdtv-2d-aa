using Godot;

[GlobalClass]
public abstract partial class CharacterClass : Resource, ICharacterClass
{
    public virtual string ClassName => throw new System.NotImplementedException();

    public virtual string Description { get; set; }

    public virtual Stat[] PrimaryStats { get; set; }

    public virtual int MaxLevel { get; set; }
}
