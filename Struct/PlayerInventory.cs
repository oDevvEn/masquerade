using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerInventory : Node
{
	[Export] public int Size = 4;

	public List<InventorySlot> Slots = new();
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0; i < Size; i++)
		{
			Slots.Add(new InventorySlot());
		}

		Slots[1].ItemData = new Coin();
	}
	
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public bool AddItem(ItemData itemData)
	{
		foreach (var slot in Slots)
		{
			if (slot.IsEmpty)
			{
				slot.ItemData = itemData;
				return true;
			} 
			else
			{
				return false;
				// pop up say no
			}
		}

		return false;
	}

	public void RemoveItem(int index)
	{
		var slot = Slots[index];

		if (slot.IsEmpty)
			return;
		else
		{
			slot.ItemData = null;
			Slots.RemoveAt(index);
			// do something to drop into environment
			
		}
	}

	public void UseItem(int index, Node2D item, Vector2 targetPos)
	{
		var slot = Slots[index];
		
		slot.ItemData.Use(item, targetPos);
		RemoveItem(index);
	}
}
