using Godot;
using System;

public partial class Interaction : VBoxContainer
{
	private RayCast2D raycast;
	private Label selectorLabel;
	
	private Node2D previousNode;
	private int selected;
	private int maxSelected;
	
	public override void _Ready() 
	{
		GD.Print(GetTree().CurrentScene);
		raycast = GetNode<RayCast2D>("../../InteractionRaycast");
		selectorLabel = GetNode<Label>("../SelectorLabel");
	}

	
	public override void _Process(double delta)
	{
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
			switch ((string)previousNode.GetMeta("type"))
			{
				case "Door":
				{
					bool open = (bool)previousNode.GetMeta("open");
					previousNode.Rotate(open ? -Mathf.Pi / 2f : Mathf.Pi / 2f);
					previousNode.SetMeta("open", !open);
					break;
				}
			}
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
