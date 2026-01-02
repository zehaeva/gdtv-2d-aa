using Godot;
using System;

public partial class CharacterAbilities : Resource
{
    [Export] public Ability Ability { get; set; }
    [Export] public Resource From { get; set; }
}
