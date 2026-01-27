using Godot;
using System;

public class InventorySlot
{
	public ItemData ItemData;
	
	public bool IsEmpty => ItemData == null;
}
