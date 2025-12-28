using System;
using Godot;

[GlobalClass]
public partial class ClassesResource : GameResource
{
    public event Action OnXPGain;
    public event Action OnXPLoss;
    public event Action OnLevelGain;
    public event Action OnLevelLoss;

    public new string ResourceType { get => GameConstants.RESOURCE_CLASS; }

    [Export] public Classes ClassType { get; private set; }
    [Export] public CharacterClass CharacterClass { get; private set; }

    //public CharacterClass CharacterClass { get; private set; }

    private int _ClassLevel;
    private int _ExperiancePoints;

    [Export]
    public int ClassLevel
    {
        get => _ClassLevel;
        set
        {
            if (value < 0)
            {
                OnLevelLoss?.Invoke();
            }
            else
            {
                OnLevelGain?.Invoke();
            }

            _ClassLevel = value;
        }
    }

    [Export]
    public int ExperiancePoints
    {
        get => _ExperiancePoints;
        set
        {
            if (value < 0)
            {
                OnXPLoss?.Invoke();
            }
            else
            {
                OnXPGain?.Invoke();
            }

            _ExperiancePoints = value;
        }
    }

    
    public void LoadClass()
    {
        //if (ClassScene == null) { return; }

        GD.Print("loading class step 1");
        //Node _class = ClassScene.Instantiate();
        GD.Print("loading class step 2");

        //switch (_class.)
        //{
        //    case GameConstants.CLASS_WIZARD:
        //        GD.Print("loading class step 3");
        //        CharacterClass = _class as Wizard;
        //        GD.Print("loading class step 4");
        //        break;
        //}
        GD.Print("loading class step 5");

    }
}
