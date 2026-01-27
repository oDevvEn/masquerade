using Godot;

[GlobalClass]

public partial class ItemData : Resource
{
    [Export] public string itemName;
    [Export] public string itemDescription;
    [Export] public Texture2D itemIcon;
}