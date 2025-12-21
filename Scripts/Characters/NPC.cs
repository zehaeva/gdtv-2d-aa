using Godot;
using System;

public partial class NPC : Character
{
    [Export] protected string npc_name = "";
    [Export] protected string[] dialogue_lines;
    [Export] protected int hp = 10;
    [Export] protected Label DialogueLabel;
    [Export] protected CanvasLayer DialogueLayer;
    [Export] protected Label NameLabel;
    protected int dialogue_index = 0;
    protected bool can_interact = false;
}
