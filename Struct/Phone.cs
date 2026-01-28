using Godot;
using System;

[GlobalClass]
public partial class Phone : ItemData
{
	public float Battery
	{
		get;
		set;
	} = 0.00f;
	public override void Use(Node2D plr, Vector2 targetPos)
	{
		if (!(Battery > 0.4f))
			return;
		else
		{
			// progress bar that takes x minutes, generates a volume collision box
			// if caught instant L, if time is over win police ending 
		}
	}

	public void Charge() // use timer on charger class that runs Charge every x minute
	{
		Battery += 0.01f;
	}
}
