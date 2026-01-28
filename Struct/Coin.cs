using Godot;
using System;

using Godot;

[GlobalClass]

public partial class Coin : ItemData
{
	public override void Use(Node2D plr, Vector2 targetPos)
	{
		//var range = plr.GetNode<CollisionShape2D>("AUDIOCUEarea/AUDIOCUE"); // !!!!!!!!! look at the player scene

		

		var coinResource = (CompressedTexture2D)ResourceLoader.Load("res://Assets/Sprite-0001.png");
		var coinSprite = new Sprite2D();
		coinSprite.Texture = coinResource;
		coinSprite.GlobalPosition = targetPos;
		plr.GetParent().AddChild(coinSprite);
		var sound = new CollisionShape2D();
		sound.Shape = new CircleShape2D();
		sound.Scale = new Vector2(50f, 50f);
		coinSprite.AddChild(sound);
	}
}
