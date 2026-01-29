using Godot;
using System;

public partial class Timer : Godot.Timer
{
	// Called when the node enters the scene tree for the first time.

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	
	void _on_timer_timeout()
	{
		GD.Print("HIII");
		var range = GetNode<CollisionShape2D>("../AUDIOCUEarea/AUDIOCUE");
		range.Disabled = true;
	}
}
