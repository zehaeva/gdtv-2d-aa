using Godot;
using System;

[GlobalClass]
public partial class Ability : Resource
{
    [Export] public string AbilityName { get; set; }
    [Export] public string Description { get; set; }
}
