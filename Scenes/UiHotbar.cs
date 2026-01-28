using Godot;
using System;

public partial class UiHotbar : Control
{
	public PlayerInventory inventory; 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		inventory = GetNode<PlayerInventory>("Player/PlayerInventory");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateUI()
	{
		foreach (Node child in this.GetChildren())
		{
			child.QueueFree();
		}
		
		for (int i = 0; i < Size.X; i++)
		{
			var slot = inventory.Slots[i];
			var button = new TextureButton();

			if (slot.ItemData != null)
			{
				button.TextureNormal = slot.ItemData.itemIcon;  // Assuming `Icon` is a property of ItemData that holds a Texture
			}
			else
			{
				button.TextureNormal = null;
			}

			this.AddChild(button);
		}
	}
}
