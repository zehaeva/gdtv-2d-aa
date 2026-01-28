using Godot;
using System;
using System.Linq;

public partial class NPC : Character
{
    [Export] public string npc_name { get; private set; } = "";
    [Export] public string[] dialogue_lines { get; private set; }
    [Export] protected int hp = 10;
    [Export] public Label DialogueLabel { get; private set; }
    [Export] public CanvasLayer DialogueLayer { get; private set; }
    [Export] public Label NameLabel { get; private set; }

    [Export] public NPCBlackboard Blackboard { get; private set; }

    public int dialogue_index { get; private set; } = 0;
    public bool can_interact { get; private set; } = false;
}
