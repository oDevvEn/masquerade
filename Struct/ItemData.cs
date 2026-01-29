using Godot;

[GlobalClass]

public partial class ItemData : Resource
{
	public string itemName;
	public string itemDescription;
	public CompressedTexture2D itemIcon;
	

	public virtual void Use(Node2D plr, Vector2 targetPos)
	{
		GD.Print("meow");
	}
}
