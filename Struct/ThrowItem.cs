using Godot;
using System;
[GlobalClass]

public partial class ThrowItem : ItemData
{
	public PackedScene ProjectileScene;

	public override void Use(Node2D plr, Vector2 targetPos)
	{
		var projectile = ProjectileScene.Instantiate<Throwable>();
		projectile.GlobalPosition = plr.GlobalPosition;
		
		Vector2 dir = (targetPos - plr.GlobalPosition).Normalized();
		
		projectile.Throw(dir);
		
		plr.GetTree().CurrentScene.AddChild(projectile);
	}
}
