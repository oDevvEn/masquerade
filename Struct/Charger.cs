using Godot;
using System;
[GlobalClass]
public partial class Charger : ItemData
{
	public void Use(Node2D plr, Phone use)
	{
		GetLocalScene().GetNode<Timer>("ChargeTimer").Start();
		// start timer, 
	}
}
