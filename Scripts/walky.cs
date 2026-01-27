using Godot;
using System;

public partial class walky : CharacterBody2D
{
	[Export] 
	public int Speed { get; set; } = 400;
	
	public void GetInput()
	{
		LookAt(GetGlobalMousePosition());
		Vector2 inputDirection = Input.GetVector("left", "right", "forward", "back");
		//GD.Print(Transform.X.AngleTo(inputDirection));
		float multi = Math.Clamp((GlobalPosition.DistanceTo(GetGlobalMousePosition()) / 500), 0f, 400f);
		//GD.Print(Speed);
		if (Math.Abs(Transform.X.AngleTo(inputDirection)) > Math.PI / 3f)
			Velocity = inputDirection * Math.Clamp((Speed/3f / multi), 0, 400);
		else
			Velocity = inputDirection * Speed;
		
		GD.Print(Math.Clamp((Speed/3f/multi), 0, 400));
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
	}
}
