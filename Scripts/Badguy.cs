using Godot;
using System;
using Godot.Collections;

public partial class Badguy : Sprite2D
{
	private float speed = 100f;
	private float visionRange = 1024f;
	private float visionField = Mathf.Pi / 3f;	
	
	private CharacterBody2D player;
	private NavigationAgent2D navAgent;
	private RayCast2D raycast;
	private Area2D audioArea;
	
	private double lastSeemTimee;
	private bool animate;
	private double timeSinceLastFrame;
	private bool playerInAttack;
	
	public override void _Ready()
	{
		Visible = false;
		player = GetTree().GetCurrentScene().GetNode<CharacterBody2D>("Player");
		navAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		raycast = GetNode<RayCast2D>("RayCast2D");
		audioArea = GetNode<Area2D>("AudioArea2D");
		Callable.From(ActorSetup).CallDeferred();
	}

	private async void ActorSetup()
	{
		await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
		//navAgent.SetTargetPosition(new Vector2(920f, 540f));1
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
	public override void _PhysicsProcess(double delta)
	{

		PhysicsRayQueryParameters2D parameters = PhysicsRayQueryParameters2D.Create(Position, player.Position, 1);
		parameters.Exclude = new Array<Rid>([this, player]);

		if (Mathf.Abs((-Transform.Y).AngleTo(player.Position-Position)) < visionField &&
			(player.Position - Position).Length() < visionRange && GetWorld2D().DirectSpaceState.IntersectRay(parameters).Count == 0)
		{
			lastSeemTimee = Time.GetUnixTimeFromSystem();
		}

		if (Time.GetUnixTimeFromSystem() - lastSeemTimee < 5D)
		{
			navAgent.TargetPosition = player.Position;
			LookAt(player.Position);
			Rotate(Mathf.Pi/2f);
		}
		
		if (!navAgent.IsNavigationFinished())
		{
			Position += Position.DirectionTo(navAgent.GetNextPathPosition());//https://docs.godotengine.org/en/stable/classes/class_raycast2d.html#class-raycast2d-method-get-collider * (float)delta * 40ssf;
			LookAt(navAgent.GetNextPathPosition());                                                                                                                                                                                                         
			Rotate(Mathf.Pi/2f);
		}

		if (raycast.IsColliding())
		{
			Node2D collision = (Node2D)((Node)raycast.GetCollider()).GetParent();
			if (collision.HasMeta("type"))
			{
				switch ((string)collision.GetMeta("type", "error"))
				{
					case "Door":
					{
						if (!(bool)collision.GetMeta("open", true))
						{
							collision.SetMeta("open", true);
							collision.Rotate(Mathf.Pi / 2f);
						}
						break;
					}
				}
			}
		}
	}

	public override void _Process(double delta)
	{
		if (animate)
		{
			timeSinceLastFrame += delta;
			if (timeSinceLastFrame > .02D)
			{
				if (Frame == 6)
				{
					animate = false;
					
				}
				else
				{
					if (Frame != 6)
						Frame++;
					timeSinceLastFrame = 0D;
				}
			}
		}
		else
		{
			if (Frame != 0)
				Frame--;
			if (playerInAttack && Frame == 0)
			{
				GetTree().ChangeSceneToFile("res://Scenes/dead.tscn");
			}
		}


		if (Time.GetUnixTimeFromSystem() - lastSeemTimee > 5D)
		{
			Array<Area2D> audioCues = audioArea.GetOverlappingAreas();
			if (audioCues.Count > 0)
				GD.Print(audioCues);
				navAgent.TargetPosition = audioCues[0].GlobalPosition;
		}
	}

	public void AttackAreaEntered(Node2D body)
	{
		animate = true;
		playerInAttack = true;
	}

	public void AttackAreaExited(Node2D body)
	{
		playerInAttack = false;
	}
}
