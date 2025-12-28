using Godot;
using System.Linq;
using System;

[GlobalClass]
public abstract partial class CharacterClass : Resource, ICharacterClass
{
    public virtual string ClassName => throw new System.NotImplementedException();

    [Export] public virtual string Description { get; set; }

    public virtual Stat[] PrimaryStats { get; set; }

    [Export] public virtual int MaxLevel { get; set; }

    [Export] public int[] XPTable { get; set; }

    public virtual bool CheckForLevelUp(Character character)
    {
        bool _return = false;

        GD.Print(String.Format("classname: {0}", ClassName));
        ClassesResource _class = character.Classes.Where(x => x.CharacterClass.ClassName == ClassName).FirstOrDefault();

        // we go this class? then figure out if we've leveled it!
        if (_class != null)
        {
            // loop through the table, starting at the current level
            for (int i = (_class.ClassLevel - 1); i < MaxLevel; i++)
            {
                GD.Print(String.Format("XPTable[{0}]: {1}", i, XPTable[i]));
                // should class levels have more criteria than just an xp point amount?
                if (XPTable[i] <= _class.ExperiancePoints)
                {
                    _return = true;
                    continue;
                }
            }
        }

        return _return;
    }
}
