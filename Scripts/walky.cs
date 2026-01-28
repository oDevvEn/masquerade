using Godot;
using System;

public partial class walky : CharacterBody2D
{
	public int Speed { get; set; } = 150;
	public float AudioMultiplier { get; set; } = 1f;

	private Breathing breathing;
	public PlayerInventory inventory;     // inventory.Additem(Item)/.RemoveItem(index),
	// inventory made of like idk 5 slots, make a hotbar for items thats just an array ig
	//  

	public float ConeAngle { get; set; } = 60f;
	public float Range = 3500;

	public override void _Process(double delta)
	{
		Observe();
	}

	private void Observe()
	{
		var objects = GetTree().GetNodesInGroup("Enemies");
		foreach (Node node in objects)
		{
			Badguy bad = node as Badguy;

			Vector2 toEnemy = bad.GlobalPosition - GlobalPosition;

			if (toEnemy.Length() > Range)
			{
				bad.Hide();
				continue;
			}
			
			Vector2 forward = (GetGlobalMousePosition() - GlobalPosition).Normalized();
			
			float angle = Mathf.RadToDeg(forward.AngleTo(toEnemy.Normalized()));

			if (MathF.Abs(angle) < ConeAngle)
			{
				if (LineOfSight(bad.GlobalPosition))
				{
					bad.Exposed();
				}
				else
				{
					bad.Hide();
				}
			}
			else
			{
				bad.Hide();
			}
		}
	}

	public bool LineOfSight(Vector2 pos)
	{
		var space = GetWorld2D().DirectSpaceState;

		var query = PhysicsRayQueryParameters2D.Create(
			GlobalPosition,
			pos
		);

		query.CollisionMask = 1;
		var result = space.IntersectRay((query));

		return result.Count == 0;
	}

	public override void _Ready()
	{
		breathing = GetNode<ColorRect>("CanvasLayer/Breathing") as Breathing;
		inventory = GetNode<Node>("Inventory") as PlayerInventory;
	}                                                                
	
	public void GetInput()
	{
		LookAt(GetGlobalMousePosition());
		Vector2 inputDirection = Input.GetVector("left", "right", "forward", "back");
		//GD.Print(Transform.X.AngleTo(inputDirection));
		float speedSlow = Math.Clamp((Speed / 1.5f), 0, 150);
		float speedCrouch = Math.Clamp(((Speed/2.5f)), 0, 150);
		float speedRun = Math.Clamp(((Speed*2)/1.1f), 0, 250);
		float currentSpeed = 0;
		//GD.Print(Speed);
		if (Math.Abs(Transform.X.AngleTo(inputDirection)) > Math.PI / 3f)
		{
			currentSpeed = speedSlow;
			Velocity = inputDirection * speedSlow;
		}
		else if (Input.IsActionPressed("sneak"))
		{
			currentSpeed = speedCrouch;
			Velocity = inputDirection * speedCrouch;
		}
		else if (Input.IsActionPressed("run"))
		{
			currentSpeed = speedRun;
			Velocity = inputDirection * speedRun;
		}
		else
		{
			currentSpeed = Speed;
			Velocity = inputDirection * Speed;    
		}
			
		
		//GD.Print(Math.Clamp((Speed/3f/SpeedMulti), 0, 400));
		
		
		

		if (Input.IsActionPressed("forward")
			|| Input.IsActionPressed("back")
				|| Input.IsActionPressed("left")
					|| Input.IsActionPressed("right"))
		{
			var range = GetNode<CollisionShape2D>("AUDIOCUEarea/AUDIOCUE");
			range.Scale = new Vector2(this.Scale.X * currentSpeed /5, this.Scale.Y * currentSpeed /5);
			range.Disabled = false;
			var timer = GetNode<Timer>("Timer");
			if (timer.IsStopped())
				timer.Start();
		}

		if (Input.IsActionJustPressed("UseItem")) // !!!!!!! isactionpressed isactionjustpressed isactionjustreleased &&& error check or something for wehtehr itemdata set in playerinv	
		{
			UseItem(1);
		}

	}

	public void UseItem(int index)
	{
		Vector2 mousePos = GetGlobalMousePosition();
		inventory.UseItem(index, this, mousePos); 
	}

	public override void _PhysicsProcess(double delta)
	{

		if (!breathing.breathing)
		{
			GetInput();
			MoveAndSlide();
		}
		
	}
}
