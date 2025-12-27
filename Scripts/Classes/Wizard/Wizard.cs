using Godot;
using System.Linq;

public partial class Wizard : Node, ICharacterClass
{
    public string ClassName => GameConstants.CLASS_WIZARD;

    public Stat[] PrimaryStats { get { return new Stat[] { Stat.Intellect }; } }

    [Export] public string Description { get; set; }

    [Export] public int MaxLevel { get; set; }

    [Export] public ClassConstraint[] ClassConstraints { get; set; }

    [Export] public int[] XPTable {  get; set; }

    public bool CheckForLevelUp(Character character)
    {
        bool _return = false;

        ClassesResource _class = character.Classes.Where(x => x.ResourceType == ClassName).FirstOrDefault();

        // loop through the table, starting at the current level
        for (int i = (_class.ClassLevel - 1); i < MaxLevel; i++)
        {
            if (XPTable[i] > _class.ExperiancePoints)
            {
                _return = true;
                continue;
            }
        }

        return _return;
    }

    public void AddLevel(Character character, int level)
    {
        // increment the character's level for this class
        foreach (var item in character.Classes.Where(x => x.ResourceType == ClassName))
        {
            item.ClassLevel++;
        };

        // add all the new abilities, if any, to the character here
    }
}
