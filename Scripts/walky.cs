using Godot;
using System;

public partial class walky : CharacterBody2D
{
	[Export] 
	public int Speed { get; set; } = 400;
	public float AudioMultiplier { get; set; } = 1f;
	
	
	
	public void GetInput()
	{
		LookAt(GetGlobalMousePosition());
		Vector2 inputDirection = Input.GetVector("left", "right", "forward", "back");
		//GD.Print(Transform.X.AngleTo(inputDirection));
		float SpeedMulti = Math.Clamp((GlobalPosition.DistanceTo(GetGlobalMousePosition()) / 500), 0f, 400f);
		//GD.Print(Speed);
		if (Math.Abs(Transform.X.AngleTo(inputDirection)) > Math.PI / 3f)
			Velocity = inputDirection * Math.Clamp((Speed/3f / SpeedMulti), 0, 400);
		else
			Velocity = inputDirection * Speed;
		
		//GD.Print(Math.Clamp((Speed/3f/SpeedMulti), 0, 400));
		
		var thing = new CollisionShape2D();
		thing.Name = "AUDIOCUE";
		thing.Shape = new CircleShape2D();


		Root.AddChild(thing);
		var ExpireAudio = GetNode("ExpireAudio");
		thing.Scale = new Vector2(this.Scale.X*2, this.Scale.Y*2);
		thing.Position = Vector2.Zero;



	}

	void _on_timer_timeout()
	{
		GD.Print("HIII");
		this.FindChild("AUDIOCUE").Free();
		this.FindChild("Timer").Free();
	}

	

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
	}
}
