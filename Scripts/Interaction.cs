using Godot;
using System;
using System.Collections.Generic;

public partial class Interaction : VBoxContainer
{
	private RayCast2D raycast;
	private Label selectorLabel;
	private CharacterBody2D player;
	private Breathing breathing;
	private Camera2D camera;
	
	private Node2D previousNode;
	private int selected;
	private int maxSelected;
	private RandomNumberGenerator rng;
	private List<AudioStream> audioStreams;
	
	public override void _Ready() 
	{
		GD.Print(GetTree().CurrentScene);
		raycast = GetNode<RayCast2D>("../../InteractionRaycast");
		selectorLabel = GetNode<Label>("../SelectorLabel");
		player = (CharacterBody2D)GetParent().GetParent();
		breathing = GetNode<ColorRect>("../Breathing") as Breathing;
		camera = GetNode<Camera2D>("../../Camera2D");
		rng = new RandomNumberGenerator();
		audioStreams = new List<AudioStream>();
		audioStreams.Add(ResourceLoader.Load<AudioStream>("res://Assets/doorcreak1.mp3"));
		audioStreams.Add(ResourceLoader.Load<AudioStream>("res://Assets/doorcreak2.mp3"));
		audioStreams.Add(ResourceLoader.Load<AudioStream>("res://Assets/doorcreak3.mp3"));
		audioStreams.Add(ResourceLoader.Load<AudioStream>("res://Assets/doorcreak4.mp3"));
		audioStreams.Add(ResourceLoader.Load<AudioStream>("res://Assets/doorcreak5.mp3"));
		audioStreams.Add(ResourceLoader.Load<AudioStream>("res://Assets/doorcreak6.mp3"));
		audioStreams.Add(ResourceLoader.Load<AudioStream>("res://Assets/doorcreak7.mp3"));
	}

	
	public override void _Process(double delta)
	{
		if (breathing.breathing) return;
		
		if (raycast.IsColliding())
		{
			Node2D collision = (Node2D)((Node)raycast.GetCollider()).GetParent();
			if (previousNode != collision)
			{
				previousNode = collision;
				selected = 0;
				selectorLabel.Position = new Vector2(1000f, 594f + selected);
				selectorLabel.Visible = true;
				ClearNodes();
				
				switch ((string)collision.GetMeta("type"))
				{
					case "Door":
					{
						Label label = new Label();
						label.Text = $"{((bool)collision.GetMeta("open") ? "Close" : "Open")} Door";
						AddChild(label);
						maxSelected = 1;
						break;
					}

					case "Wardrobe":
					{
						Label label = new Label();
						label.Text = $"Enter Wardrobe";
						AddChild(label);
						maxSelected = 1;
						break;
					}

					default:
					{
						selectorLabel.Visible = false;
						break;
					}
				}
			}
		}
		else
		{
			previousNode = null;
			selectorLabel.Visible = false;
			ClearNodes();
		}

		if (Input.IsActionJustReleased("interact") && previousNode != null)
		{
			switch ((string)previousNode.GetMeta("type", "error"))
			{
				case "Door":
				{
					bool open = (bool)previousNode.GetMeta("open");
					previousNode.Rotate(open ? -Mathf.Pi / 2f : Mathf.Pi / 2f);
					previousNode.SetMeta("open", !open);
					AudioStreamPlayer2D audio = previousNode.GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
					audio.Stream = audioStreams[rng.RandiRange(0, audioStreams.Count - 1)];
					audio.Play();
					break;
				}
				case "Wardrobe":
				{
					player.Visible = false;
					camera.Enabled = false;
					Camera2D wardrobeCamera = previousNode.GetNode<Camera2D>("Camera2D");
					breathing.wardrobeCamera = wardrobeCamera;
					wardrobeCamera.Enabled = true;
					breathing.Breathe();
					breathing.Visible = true;
					break;
				}
			}
			
			previousNode = null;
			selectorLabel.Visible = false;
			ClearNodes();
		}

		if (Input.IsActionJustReleased("interactCycle"))
		{
			selected += 1;
			if (selected >= maxSelected) selected = 0;
			else if (selected < 0) selected = maxSelected;
			selectorLabel.Position = new Vector2(1000f, 594f + 27f * selected);

		}
	}

	
	private void ClearNodes() {
		foreach (Node node in GetChildren())
			node.Free();
	}
}
