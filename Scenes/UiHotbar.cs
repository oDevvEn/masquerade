using Godot;
using System;

public partial class UiHotbar : Control
{
	public PlayerInventory inventory;
	private HBoxContainer container;
	private Node2D player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		inventory = GetNode<Node>("../../Inventory") as PlayerInventory;
		container = GetNode<HBoxContainer>("HBoxContainer");
		player = (Node2D)GetParent().GetParent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateUI()
	{
		foreach (Node child in container.GetChildren())
		{
			child.QueueFree();
		}
		
		for (int i = 0; i < inventory.Slots.Count; i++)
		{
			var slot = inventory.Slots[i];
			var button = new TextureButton();
			button.CustomMinimumSize = new Vector2(32f, 32f); // ?
			
			if (slot.ItemData != null)
			{
				button.TextureNormal = slot.ItemData.itemIcon;  // Assuming `Icon` is a property of ItemData that holds a Texture
				button.Pressed += delegate { slot.ItemData.Use(player, GetGlobalMousePosition()); };
			}
			else
			{
				button.TextureNormal = null;
			}

			container.AddChild(button);
		}
	}
}
