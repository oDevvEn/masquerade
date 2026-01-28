using Godot;
using System;

public partial class Badguy : Sprite2D
{
	private NavigationAgent2D navAgent;
	private RayCast2D raycast;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Visible = false;
		navAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		raycast = GetNode<RayCast2D>("RayCast2D");
		Callable.From(ActorSetup).CallDeferred();
	}

	private async void ActorSetup()
	{
		await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
		navAgent.SetTargetPosition(new Vector2(920f, 540f));
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
		if (!navAgent.IsNavigationFinished())
		{
			Position += Position.DirectionTo(navAgent.GetNextPathPosition()) * (float)delta * 40f;
			LookAt(navAgent.GetNextPathPosition());
			Rotate(Mathf.Pi/2f);
		}

		if (raycast.IsColliding())
		{
			Node2D collision = (Node2D)((Node)raycast.GetCollider()).GetParent();
			if (collision.HasMeta("type"))
			{
				GD.Print(collision.GetMeta("type"));
				GD.Print(collision.GetMeta("type", "error"));
				switch ((string)collision.GetMeta("type", "error"))
				{
					case "Door":
					{
						GD.Print("door");
						if (!(bool)collision.GetMeta("open", true))
						{
							GD.Print("omg!!");
							collision.SetMeta("open", true);
							collision.Rotate(Mathf.Pi / 2f);
						}
						break;
					}
				}
			}
		}

	}
}
