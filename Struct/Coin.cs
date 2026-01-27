using Godot;
using System;

using Godot;

[GlobalClass]

public partial class Coin : ItemData
{
	public override void Use(Node plr, Vector2 targetPos)
	{
		var range = plr.GetNode<CollisionShape2D>("AUDIOCUE");
		range.Position = targetPos;
		range.Disabled = false;
	}
}
