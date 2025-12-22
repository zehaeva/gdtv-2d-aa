using Godot;
using System;

public partial class AttackHitBox : Area2D, IHitBox
{
	public float GetDamage()
	{
		return GetOwner<Character>().GetStatResource(Stat.Strength).StatValue;
	}

	public bool CanStun() { return true; }
}
