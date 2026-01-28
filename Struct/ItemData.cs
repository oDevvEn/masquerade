using Godot;

[GlobalClass]

public partial class ItemData : Resource
{
	[Export] public string itemName;
	[Export] public string itemDescription;
	[Export] public Texture2D itemIcon;
	

	public virtual void Use(Node2D plr, Vector2 targetPos)
	{
		GD.Print("meow");
	}
}
