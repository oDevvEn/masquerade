using Godot;
using System;

public partial class WinCollision : Area2D
{
	private void OnCollision(Node2D body)
	{
		//if (body == GetTree().Root.GetNode("Player"))
		//{
			GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
		//}
	}
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
