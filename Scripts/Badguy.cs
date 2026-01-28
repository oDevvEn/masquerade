using Godot;
using System;

public partial class Badguy : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Visible = false;
	}

	public void Exposed()
	{
		Visible = true;
	}

	public void Hidden()
	{
		Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
