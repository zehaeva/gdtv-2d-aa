using Godot;

[GlobalClass]
public partial class ExistConstraint : ResourceConstraint
{
    [Export] public bool Exists { get; set; }

    public bool IsMet(Character character)
    {
        bool _return = false;
        object resource = null;

        switch (this.Resource.ResourceType)
        {
            case GameConstants.RESOURCE_STAT:
                resource = character.GetStatResource((Resource as StatResource).StatType).StatValue;
                break;
            case GameConstants.RESOURCE_ACHIEVEMENT:
                resource = character.GetAchievementResource((Resource as AchievementResource).AchievementType).AchivementValue;
                break;
            case GameConstants.RESOURCE_CLASS:
                resource = character.GetClassesResource((Resource as ClassesResource).ClassType).ClassType;
                break;
        }

        if (!(resource is null))
        {
            _return = true;
        }

        return _return;
    }
}
